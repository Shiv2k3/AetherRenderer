using Core.Util;
using System;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Rendering.Octree
{
    [BurstCompile(FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.FastCompilation, FloatPrecision = FloatPrecision.High)]
    public unsafe struct SparseOctree : IJobParallelFor
    {
        [NativeDisableUnsafePtrRestriction, NativeDisableContainerSafetyRestriction] public Node root;
        [NativeDisableContainerSafetyRestriction] public NativeArray<NativeList<float3>> featurePoints;

        public readonly static SharedStatic<Pool<Node>> nodePool = SharedStatic<Pool<Node>>.GetOrCreate<Pool<Node>, Node>();
        public readonly static SharedStatic<Pool<Voxel>> voxelPool = SharedStatic<Pool<Voxel>>.GetOrCreate<Pool<Voxel>, Voxel>();
        public readonly static SharedStatic<Settings> settings = SharedStatic<Settings>.GetOrCreate<Settings, SparseOctree>();
        Unity.Mathematics.Random rng;
        public SparseOctree(in Settings settings)
        {
            SparseOctree.settings.Data = settings;
            nodePool.Data = new(settings.poolDepth);
            voxelPool.Data = new(settings.poolDepth);
            featurePoints = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);

            for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
            {
                featurePoints[i] = new(8192, Allocator.Persistent);
            }
            rng = new Unity.Mathematics.Random(1);
            root = new();
            CreateRoot();
        }
        private void CreateRoot()
        {
            root = *nodePool.Data.Lease(1);
            nodePool.Data.Lease(8, out int _start);
            int _voxelStart = voxelPool.Data.Lease(8, out Voxel* _data);
            int _map = 0;
            for (int octant = 0; octant < 8; octant++)
            {
                if (CalculateVoxel(SubnodePosition(octant, 1, 0), 1, out _data[octant]))
                {
                    _map |= 1 << octant;
                }
                if (_data[octant].LatticeIndex != voxelPool.Data[_voxelStart + octant].LatticeIndex)
                    throw new("Lattice index doesn't match after update between pool and lease");

            }
            root.AssignSubnodes(_start, _voxelStart, 255, (byte)_map);
        }
        public readonly void Dispose()
        {
            nodePool.Data.Dispose();
            voxelPool.Data.Dispose();
            foreach (var item in featurePoints)
                item.Dispose();
            featurePoints.Dispose();

            Debug.Log("Disposed Octree");
        }
        public void Reset()
        {
            nodePool.Data.Dispose();
            voxelPool.Data.Dispose();

            nodePool.Data = new(settings.Data.poolDepth);
            voxelPool.Data = new(settings.Data.poolDepth);

            CreateRoot();
        }

        public void Execute(int index)
        {
            var vertices = featurePoints[JobsUtility.ThreadIndex];
            vertices.Clear();
            var orphanVoxels = new NativeHashMap<int, Voxel>(0, Allocator.TempJob);

            Subdivide(ref nodePool.Data.GetWithoutChecks(root._nodeIndex + index), 1, SubnodePosition(index, 1, 0), Voxel.TotalLatticeNodes / 2 + Table.IndexMovement[1][index], ref orphanVoxels);
            foreach (var voxel in orphanVoxels)
            {
                vertices.Add(voxel.Value.Position);
            }
            featurePoints[JobsUtility.ThreadIndex] = vertices;
            orphanVoxels.Dispose();
        }
        private readonly void Subdivide(ref Node node, in int depth, in float3 position, in int latticeIndex, ref NativeHashMap<int, Voxel> orphanVoxels)
        {
            // Exit if the subnodes would be past the max depth, > 9 
            var _depth = depth + 1;
            if (_depth > Util.Settings.MaxDepth)
            {
                return;
            }

            if (false && _depth == Util.Settings.MaxDepth) // The subnodes are will be at max depth, == 9
            {
                // Check if there are any orphan nodes that belongs to this node
                var _map = 0; // node and voxel map of the node since a node can only exist with a voxel at max depth
                var tempData = new NativeList<Voxel>(8, Allocator.Temp);
                for (int octant = 0; octant < 8; octant++)
                {
                    int index = latticeIndex + Table.IndexMovement[_depth][octant]; // the lattice index of the subnode
                    if (orphanVoxels.ContainsKey(index)) // Is the node in the map?
                    {
                        _map |= 1 << octant;
                        tempData.AddNoResize(orphanVoxels[index]);
                        orphanVoxels.Remove(index);
                        Debug.Log("Found a orphan at " + index);
                    }
                }

                // Lease the voxles and subnodes for this node
                int dataStartIndex = voxelPool.Data.Lease(tempData.Length, out Voxel* _dataStart);
                Buffer.MemoryCopy(tempData.GetUnsafePtr(), _dataStart, Voxel.ByteSize * tempData.Length, Voxel.ByteSize * tempData.Length);
                nodePool.Data.Lease(tempData.Length, out int _nodeStart);
                node.AssignSubnodes(_nodeStart, dataStartIndex, (byte)_map, (byte)_map);
                return;
            }
            else // Subnodes will be above max dpeth, <= 8
            {
                var _map = 0;
                var _dataMap = 0;
                var tempData = new NativeList<Voxel>(8, Allocator.Temp);
                var tempPosition = new NativeList<float3>(8, Allocator.Temp);
                for (int octant = 0; octant < 8; octant++)
                {
                    float3 _position = SubnodePosition(octant, _depth, position);
                    bool octantInLod = _depth <= Util.Settings.MaxDepth - SubnodeLOD(_position);
                    //if (!octantInLod) continue; // Reduces the resudial voxels calculated

                    bool pointInOctant = CalculateVoxel(_position, _depth, out var _data);
                    bool pointInLod = SubnodeLOD(_data.Position) <= Util.Settings.MaxDepth;
                    int _dataLatticeIndex = _data.LatticeIndex;
                    if (octantInLod && pointInOctant)
                    {
                        _map |= 1 << octant;
                        tempPosition.AddNoResize(_position);

                        // Only claim octant if this non-max-depth node's lattice index matches with the voxel's 
                        //if (_dataLatticeIndex == IndexPosition.CellIndex(_position, Voxel.LatticeResolution))
                        {
                            _dataMap |= 1 << octant;
                            tempData.AddNoResize(_data);
                            orphanVoxels.TryAdd(_dataLatticeIndex, _data);
                        }
                        //else // Add voxel to orphan map if the voxel is within this octant
                        //{
                        //    orphanVoxels.TryAdd(_dataLatticeIndex, _data);
                        //}
                    }
                    //else if (pointInLod)
                    //{
                    //    orphanVoxels.TryAdd(_dataLatticeIndex, _data);
                    //    _map |= 1 << octant;
                    //}
                }

                if (tempPosition.Length != 0)
                {
                    int dataStartIndex = tempData.Length; // 0 if it tempData is empty
                    if (tempData.Length > 0)
                    {
                        dataStartIndex = voxelPool.Data.Lease(tempData.Length, out Voxel* _dataStart);
                        Buffer.MemoryCopy(tempData.GetUnsafePtr(), _dataStart, Voxel.ByteSize * tempData.Length, Voxel.ByteSize * tempData.Length);
                    }
                    nodePool.Data.Lease(tempPosition.Length, out int _nodeStart);
                    node.AssignSubnodes(_nodeStart, dataStartIndex, (byte)_map, (byte)_dataMap);
                    for (int bit = 0, start = 0; bit < tempPosition.Length; bit++)
                    {
                        FindNext1Bit(_map, ref start, out int index);
                        start = index + 1;
                        Subdivide(ref node.Subnode(bit), _depth, tempPosition[bit], latticeIndex + Table.IndexMovement[_depth][index], ref orphanVoxels);
                    }
                }
            }
        }

        private readonly bool CalculateVoxel(float3 position, in int depth, out Voxel data)
        {
            float3 range = (Table.HalfSubnodeLength[depth] * settings.Data.samplingOffset) / (math.normalize(position) / settings.Data.sampleSpread); 
            position += rng.NextFloat3(-range, range);
            float sD = SDFs.SDSphere(position, settings.Data.sphereRadius, out var sG);
            float3 surfacePoint = -sD * sG + position;
            if (math.abs(SDFs.SDSphere(surfacePoint, settings.Data.sphereRadius, out _)) > 0.001f)
                throw new("Surface point isn't on the surface");
            //float nD = noise.snoise(surfacePoint, out var nG);
            //surfacePoint -= nD * nG;
            //data = new(surfacePoint, 0, sG + nG);
            data = new(surfacePoint, 0, sG);

            return SDFs.SDBox(surfacePoint - position, Table.HalfSubnodeLength[depth]) <= 0;
            //return math.all(surfacePoint >= position - Table.HalfSubnodeLength[depth]) && math.all(surfacePoint <= position + Table.HalfSubnodeLength[depth]);
        }
        public readonly float SubnodeLOD(in float3 position)
        {
            // math.log2(SubnodeLength(depth) / SubnodeLength(Settings.MaxDepth))
            float distLod = math.distance(settings.Data.octreeCenter, position);
            distLod /= Util.Settings.HalfWorldSize; // normalize to world size
            distLod *= settings.Data.lodFactor; // upper bound is the lod factor
            return distLod;
        }
        public static float3 SubnodePosition(in int index, in int depth, in float3 parentempPosition)
        {
            float3 childPosition = Table.CubeCorners[index];
            childPosition *= Table.SubnodeLength[depth];
            return childPosition + parentempPosition;
        }
        public static void FindNext1Bit(in int map, ref int start, out int next1BitIndex)
        {
#if DEBUG
            if (map == 0 || start < 0 || start > 7) throw new("Invalid bitmap or index input");
#endif
            for (int i = start; i < 8; i++)
            {
                int bit = 1 << i;
                if ((bit & map) == bit)
                {
                    next1BitIndex = i;
                    return;
                }
            }
            throw new("Unable to find an on bit");
        }
        public static float SubnodeLength(in int octantDepth)
        {
            return Table.SubnodeLength[octantDepth];
        }
        public readonly int NodesUsed()
        {
            int count = 0;
            for (int i = 0; i < nodePool.Data.lengths.Length; i++)
            {
                count += nodePool.Data.lengths[i];
            }
            return nodePool.Data.lengths.Length * nodePool.Data.nodesPerThread - count;
        }
    }
}