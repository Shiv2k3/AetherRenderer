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

        internal readonly static SharedStatic<ChildrenPool> OctantPool = SharedStatic<ChildrenPool>.GetOrCreate<ChildrenPool, Node>();
        internal readonly static SharedStatic<WorldDiscriptor> World = SharedStatic<WorldDiscriptor>.GetOrCreate<WorldDiscriptor, SparseOctree>();

        public SparseOctree(in WorldDiscriptor worldDiscriptor)
        {
            root = new(0,0);
            OctantPool.Data = new ChildrenPool(32768);
            World.Data = worldDiscriptor;
            
            root.Divide();
            octants = root.Octants;
        }

        public void Execute(int index)
        {
            // Get the current thread's octant
            Node octant = octants.nodes[index];
            // Subdivide the octant
            Subdivide(ref octant);
            // Set the octant on the root
            octants.nodes[index] = octant;
        }

        /// <summary>
        /// Continuously divides given node until max depth is reached
        /// </summary>
        /// <param name="node">The node to divide, should not be previously divided</param>
        private readonly void Subdivide(ref Node node)
        {
            if (node.Depth >= World.Data.maxDepth) return;

            // Check if it has stuff
            float distance = SDFs.SDSphere(node.Position, World.Data.sphereRadius, out _);
            float octantLength = 1f / Mathf.Pow(2, node.Depth) * World.Data.rootLength;
            float octantDiagonal = octantLength * math.sqrt(3);
            if (math.abs(distance) > octantDiagonal * 0.5f) return;  

            node.Divide();
            Children octants = node.Octants;
            for (int i = 0; i < 8; i++)
            {
                Node octant = octants.nodes[i];
                Subdivide(ref octant);
                octants.nodes[i] = octant;
            }
            node.Octants = octants;
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

            OctantPool.Data.Dispose();
            Debug.Log("Disposed node pool");

            static void ReleaseChildren(in Node node, ref int disposeCount)
            {
                if (!node.IsValid) return;

                if (!node.IsLeaf)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        ReleaseChildren(node.Octants.nodes[i], ref disposeCount);
                    }
                }

                node.Release();
                disposeCount++;
            }
        }
    }
}