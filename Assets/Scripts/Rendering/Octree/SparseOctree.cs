using Core.Util;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
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

        internal readonly static SharedStatic<ChildrenPool> ChildrenPool = SharedStatic<ChildrenPool>.GetOrCreate<ChildrenPool, Node>();
        internal readonly static SharedStatic<WorldDiscriptor> World = SharedStatic<WorldDiscriptor>.GetOrCreate<WorldDiscriptor, SparseOctree>();

        public SparseOctree(in WorldDiscriptor worldDiscriptor)
        {
            root = new(0,0);
            ChildrenPool.Data = new ChildrenPool(7000);
            World.Data = worldDiscriptor;
            
            root.Divide(255);
            octants = root.Children;
            Camera = 0;
        }

        public void Execute(int index)
        {
            // Get the current thread's octant
            Node octant = octants.Nodes[index];
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
            float distToCam = math.distance(Camera, node.Position) / (World.Data.rootLength / 2f) * World.Data.maxDepth;
            //float nodeLOD = math.log2(OctantLength(node.Depth) / OctantLength(World.Data.maxDepth)) * distToCam;
            float nodeLOD = math.clamp(distToCam, 0, World.Data.maxDepth);
            if (node.Depth >= World.Data.maxDepth - (int)nodeLOD)
            {
                // If node has children, they must be released
                if (!node.Children.IsEmpty)
                {
                    node.Children.Release();
                    node.Children = Children.Empty;
                }

                return;
            }

            // Check which of the possible child nodes intersect a surface
            byte active = 0;
            int octantDepth = node.Depth + 1;
            for (int i = 0; i < 8; i++)
            {
                float3 position = OctantPosition(i, octantDepth, node.Position);
                if (NodeHasSurface(position, octantDepth))
                    active |= (byte)(1 << i);
            }
            if (active == 0) return;

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

                node.Release();
                disposeCount++;
            }
        }
    }
}