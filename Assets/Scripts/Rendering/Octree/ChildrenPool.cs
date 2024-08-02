using Unity.Collections;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace Core.Octree
{
    public struct ChildrenPool
    {
        private NativeArray<Node> _nodePool;
        public NativeArray<NativeArray<NativeList<Children>>> _thread_size_children;
        public NativeArray<NativeList<Children>> _thread_released;

        public ChildrenPool(int childrenPerThread)
        {
            _nodePool = new(childrenPerThread * 36 * JobsUtility.ThreadIndexCount, Allocator.Persistent);
            _thread_size_children = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);
            _thread_released = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);

            // Each thread has its own segment of children, threadSegment
            // Each threadSegment has 8 pools of children organized their size, sizeSegment
            // Each sizeSegment has a childrenPerSize elements, every children has the same number of nodes
            for (int thread = 0, poolIndex = 0; thread < JobsUtility.ThreadIndexCount; thread++)
            {
                _thread_released[thread] = new(childrenPerThread / 8, Allocator.Persistent);

                NativeArray<NativeList<Children>> threadSegment = new(8, Allocator.Persistent);
                for (int size = 1; size <= 8; size++)
                {
                    NativeList<Children> sizeSegment = new(childrenPerThread, Allocator.Persistent);
                    for (int child = 0; child < childrenPerThread; child++, poolIndex += size)
                    {
                        var nodes = _nodePool.GetSubArray(poolIndex, size);
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            nodes[i] = Node.Invalid;
                        }
                        Children c = new(nodes, (byte)thread);
                        sizeSegment.AddNoResize(c);
                    }
                    sizeSegment.TrimExcess();
                    threadSegment[size - 1] = sizeSegment;
                }
                _thread_size_children[thread] = threadSegment;
            }

#if UNITY_EDITOR
            int t = 0;
            foreach (var threadSeg in _thread_size_children)
            {
                int size = 1;
                foreach (var sizeSeg in threadSeg)
                {
                    if (size > 8) throw new("More than 8 size segments");
                    if (sizeSeg.Length != childrenPerThread) throw new("Incorrect size segment length");
                    foreach (var children in sizeSeg)
                    {
                        if (children.Thread != t || children.Count != size || children.IsEmpty == false)
                            throw new("Error: Children were not created properly");
                    }
                    size++;
                }
                t++;
            }
#endif
        }

        public Children Get(int nodeCount)
        {
            var threadSeg = _thread_size_children[JobsUtility.ThreadIndex];
            var sizeSeg = threadSeg[nodeCount - 1];
            int lastChildren = sizeSeg.Length - 1;
            if (lastChildren > -1)
            {
                var children = sizeSeg[lastChildren];
                sizeSeg.RemoveAt(lastChildren);
                threadSeg[nodeCount - 1] = sizeSeg;
                _thread_size_children[JobsUtility.ThreadIndex] = threadSeg;

                return children;
            }

            throw new($"Pool out of children, [{JobsUtility.ThreadIndex},{nodeCount}]");
        }
        public unsafe void Release(ref Children children)
        {
            ReleaseChildren(ref children);
            var threadSeg = _thread_size_children[children.Thread];
            var sizeSeg = threadSeg[children.Count - 1].AsParallelWriter();
            sizeSeg.AddNoResize(children);

            static void ReleaseChildren(ref Children children)
            {
                children.IsEmpty = true;
                for (int i = 0; i < children.Count; i++)
                {
                    if (!children.Nodes[i].IsValid) continue;
                    children[i].ReleaseNode();
                    children[i] = Node.Invalid;
                }
            }
        }

        public void ReleaseThread(int thread = -1)
        {
            thread = thread == -1 ? JobsUtility.ThreadIndex : thread;
            var list = _thread_released[thread];
            for (int i = 0; i < list.Length; i++)
            {
                var children = list[i];
                Release(ref children);
            }
            _thread_released[thread].Clear();
        }
        public void Dispose()
        {
            _thread_size_children.Dispose();
            _nodePool.Dispose();
        }
    }
}