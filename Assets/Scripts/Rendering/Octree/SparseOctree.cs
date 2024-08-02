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
        public float3 Camera;

        public readonly static SharedStatic<Pool> Pool = SharedStatic<Pool>.GetOrCreate<Pool, Node>();
        internal readonly static SharedStatic<WorldDiscriptor> World = SharedStatic<WorldDiscriptor>.GetOrCreate<WorldDiscriptor, SparseOctree>();

        [NativeDisableContainerSafetyRestriction] private NativeArray<NativeList<float3>> _thread_currentChildren;

        public SparseOctree(in WorldDiscriptor worldDiscriptor)
        {
            World.Data = worldDiscriptor;
            Pool.Data = new Pool(worldDiscriptor.childrenPerThread);

            _thread_currentChildren = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);
            for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
            {
                _thread_currentChildren[i] = new(8, Allocator.Persistent);
            }

            root = *Pool.Data.Lease(1);
            Camera = 0;
        }

        public void Execute(int index)
        {
            Subdivide(ref root[index], 1, SubnodePosition(index, 1, 0));
        }

        private readonly void Subdivide(ref Node node, in int depth, in float3 position)
        {
            var _depth = depth + 1;
            if (_depth == World.Data.maxDepth)
            {
                Pool.Data.Lease(out int index, out Voxel* data);
                node.AssignChunk(index, data);
                return;
            }
        }
        private readonly bool Evaluate(in float3 position, in int depth)
        {
            float distance = SDFs.SDSphere(position, World.Data.sphereRadius, out _);
            return math.abs(distance) <= Table.OctantHalfDiagonal[depth] * World.Data.rootLength;
        }
        private readonly float SubnodeLOD(in float3 position)
        {
            float distToCam = math.distance(Camera, position) / (World.Data.rootLength / 2f) * World.Data.maxDepth;
            float nodeLOD = math.clamp(distToCam, 0, World.Data.maxDepth);
            return nodeLOD;
        }
        public static float3 SubnodePosition(in int octantIndex, in int octantDepth, in float3 parentPosition)
        {
            float3 childPosition = Table.LocalOctantPosition[octantIndex];
            childPosition *= Table.InvPowersOfTwo[octantDepth] * World.Data.rootLength;
            childPosition += parentPosition;
            return childPosition;
        }
        public static float SubnodeLength(in int octantDepth)
        {
            return Table.InvPowersOfTwo[octantDepth] * World.Data.rootLength;
        }

        public void Dispose()
        {

        }
    }
}