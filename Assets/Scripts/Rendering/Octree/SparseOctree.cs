using Core.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Rendering.Octree
{
    [BurstCompile(OptimizeFor = OptimizeFor.Performance)]
    public unsafe struct SparseOctree : IJobParallelFor
    {
        [NativeDisableUnsafePtrRestriction, NativeDisableContainerSafetyRestriction] public Node root;
        public readonly int NodesAvailable()
        {
            int count = 0;
            foreach (var length in Pool.Data.lengths)
            {
                count += length;
            }
            return count;
        }

        public readonly static SharedStatic<Pool> Pool = SharedStatic<Pool>.GetOrCreate<Pool, Node>();
        public readonly static SharedStatic<Settings> Settings = SharedStatic<Settings>.GetOrCreate<Settings, SparseOctree>();
        public SparseOctree(in Settings settings)
        {
            Settings.Data = settings;
            Pool.Data = new Pool(settings.poolDepth);

            root = *Pool.Data.Lease(1);
            root.AssignSubnodes(Pool.Data.Lease(8), 255);
        }
        public readonly void Dispose()
        {
            Pool.Data.Dispose();
            Debug.Log("Disposed Octree");
        }

        public void ResetPool()
        {
            Pool.Data.Dispose();
            Pool.Data = new(Settings.Data.poolDepth);

            root = *Pool.Data.Lease(1);
            root.AssignSubnodes(Pool.Data.Lease(8), 255);
        }
        public void Execute(int index)
        {
            Subdivide(ref root[index], 1, SubnodePosition(index, 1, 0));
        }

        private void Subdivide(ref Node node, in int depth, in float3 position)
        {
            // Check if on the chunk depth
            //   Assign a chunk to the node and exit
            var _depth = depth + 1;
            if (_depth == Util.Settings.MaxDepth)
            {
                Pool.Data.Lease(out int index, out Voxel* data);
                node.AssignChunk(index, data);
                return;
            }

            // Nodes from here internal nodes
            // Calculate basic node information
            var _count = 0; // number of subnodes
            //var _unifrom = 0; // bitmap of nodes with same voxel type as parent
            var _map = 0; // bitmap of active nodes
            var tempData = new NativeList<Voxel>(8, Allocator.Temp);
            var tempPosition = new NativeList<float3>(8, Allocator.Temp);
            for (int i = 0; i < 8; i++)
            {
                float3 _position = SubnodePosition(i, _depth, position);
                if (Evaluate(_position, _depth, out Voxel _data) && _depth <= Util.Settings.MaxDepth - SubnodeLOD(_position))
                {
                    _map |= 1 << i;

                    if (_data.id == node.data.id)
                    {
                        //_unifrom |= 1 << i;
                    }

                    tempData.AddNoResize(_data);
                    tempPosition.AddNoResize(_position);
                    _count++;
                }
            }

            // Only continue if there are subnodes and they are all not the same
            if (_count > 0)
            {
                Node* _nodes = Pool.Data.Lease(_count);
                node.AssignSubnodes(_nodes, (byte)_map);

                // Assign data and subdivide the subnodes
                for (int i = 0; i < _count; i++)
                {
                    node[i].data = tempData[i];
                    Subdivide(ref _nodes[i], _depth, tempPosition[i]);
                }

                tempData.Dispose();
                tempPosition.Dispose();
            }
            else return;
        }
        private readonly bool Evaluate(in float3 position, in int depth, out Voxel data)
        {
            float distance = SDFs.SDSphere(position, Settings.Data.sphereRadius, out _);
            data = new();// { id = (byte)(math.clamp(noise.snoise(position), 0, 1) * 255) };
            return math.abs(distance) <= Table.HalfDiagonal[depth];
        }
        public readonly float SubnodeLOD(in float3 position)
        {
            float distToCam = math.distance(Settings.Data.octreeCenter, position) / Util.Settings.HalfWorldSize * Util.Settings.MaxDepth;
            return math.clamp(distToCam, 0, Util.Settings.MaxDepth) * Settings.Data.lodFactor;
        }
        public static float3 SubnodePosition(in int index, in int depth, in float3 parentPosition)
        {
            float3 childPosition = Table.UnitCorners[index];
            childPosition *= Table.SubnodeLength[depth];
            childPosition += parentPosition;
            return childPosition;
        }
        public static void FindNext1(in int map, ref int start, out int next1BitIndex)
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
    }
}