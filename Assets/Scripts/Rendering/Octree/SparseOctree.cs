using Core.Util;
using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Octree
{
    [BurstCompile(OptimizeFor = OptimizeFor.Performance)]
    public unsafe struct SparseOctree : IJobParallelFor
    {
        [NativeDisableUnsafePtrRestriction, NativeDisableContainerSafetyRestriction] public Node root;
        [NativeDisableUnsafePtrRestriction, NativeDisableContainerSafetyRestriction] public Children octants;
        public float3 Camera;

        public readonly static SharedStatic<ChildrenPool> ChildrenPool = SharedStatic<ChildrenPool>.GetOrCreate<ChildrenPool, Node>();
        internal readonly static SharedStatic<WorldDiscriptor> World = SharedStatic<WorldDiscriptor>.GetOrCreate<WorldDiscriptor, SparseOctree>();

        [NativeDisableContainerSafetyRestriction] private NativeArray<NativeList<float3>> _thread_currentChildren;

        public SparseOctree(in WorldDiscriptor worldDiscriptor)
        {
            root = new(0, 0);
            ChildrenPool.Data = new ChildrenPool(7_000);
            World.Data = worldDiscriptor;

            _thread_currentChildren = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);
            for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
            {
                _thread_currentChildren[i] = new(0, Allocator.Persistent);
            }

            root.Divide(255);
            octants = root.Children;
            Camera = 0;
        }

        public void Execute(int index)
        {
            // Get the current thread's octant
            Node octant = octants[index];
            // Subdivide the octant
            Subdivide(ref octant);
            // Set the octant on the root
            octants.Nodes[index] = octant;
        }

        /// <summary>
        /// Continuously divides given node until max depth is reached
        /// </summary>
        /// <param name="node">The node to divide, should not be previously divided</param>
        private readonly void Subdivide(ref Node node)
        {
            // Get a list of the current children
            var currentChildren = _thread_currentChildren[JobsUtility.ThreadIndex];
            if (currentChildren.Length > 0) throw new("Previous use indicates not all children were found");
            if (!node.IsLeaf)
            {
                for (int i = 0; i < node.Children.Count; i++)
                {
                    if (node.Children[i].IsValid)
                        currentChildren.Add(node.Children[i].Position);
                }
            }

            // Check which of the possible child nodes intersect a surface and is within render distance
            byte active = 0;
            byte prevActive = 0;
            int octantDepth = node.Depth + 1;
            for (int i = 0; i < 8; i++)
            {
                float3 position = OctantPosition(i, octantDepth, node.Position);

                for (int c = 0; c < currentChildren.Length; c++)
                {
                    if (math.all(currentChildren[c] == position))
                    {
                        currentChildren.RemoveAt(c);
                        prevActive |= (byte)(1 << i);
                        break;
                    }
                }

                bool inRange = octantDepth <= World.Data.maxDepth - (int)OctantLOD(position);
                if (inRange && NodeHasSurface(position, octantDepth))
                    active |= (byte)(1 << i);
            }

            if (active == 0)
            {
                if (!node.IsLeaf)
                {
                    var c = node.Children;
                    ChildrenPool.Data.Release(ref c);
                    node.Children = c;
                }

                return;
            }

            if (active != prevActive && !node.IsLeaf)
            {
                var c = node.Children;
                ChildrenPool.Data.Release(ref c);
                node.Children = c;
            }

            // Divide to have the surface-intersecting children
            node.Divide(active);
            Children children = node.Children;
            for (int i = 0; i < children.Count; i++)
            {
                Node child = children.Nodes[i];
                Subdivide(ref child);
                children.Nodes[i] = child;
            }
            node.Children = children;

            static bool NodeHasSurface(in float3 position, in int depth)
            {
                float distance = SDFs.SDSphere(position, World.Data.sphereRadius, out _);
                return math.abs(distance) <= Table.OctantHalfDiagonal[depth] * World.Data.rootLength;
            }
        }

        private readonly float OctantLOD(in float3 position)
        {
            float distToCam = math.distance(Camera, position) / (World.Data.rootLength / 2f) * World.Data.maxDepth;
            float nodeLOD = math.clamp(distToCam, 0, World.Data.maxDepth);
            return nodeLOD;
        }
        public static float3 OctantPosition(in int octantIndex, in int octantDepth, in float3 parentPosition)
        {
            float3 childPosition = Table.LocalOctantPosition[octantIndex];
            childPosition *= Table.InvPowersOfTwo[octantDepth] * World.Data.rootLength;
            childPosition += parentPosition;
            return childPosition;
        }
        public static float OctantLength(in int octantDepth)
        {
            return Table.InvPowersOfTwo[octantDepth] * World.Data.rootLength;
        }

        public void Dispose()
        {
            int totalNodesDisposed = 0;
            if (root.IsValid)
            {
                ReleaseChildren(root, ref totalNodesDisposed);
                Debug.Log($"Octree with {totalNodesDisposed} nodes released");
            }
            octants = Children.Empty;
            root = Node.Invalid;

            ChildrenPool.Data.Dispose();
            Debug.Log("Disposed node pool");

            static void ReleaseChildren(in Node node, ref int disposeCount)
            {
                if (!node.IsValid) return;

                if (!node.IsLeaf)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        ReleaseChildren(node.Children.Nodes[i], ref disposeCount);
                    }
                }

                node.ReleaseNode();
                disposeCount++;
            }
        }
    }
}