using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Core.Octree
{
    internal struct ChildrenPool
    {
        private NativeArray<Node> _nodePool;
        private UnsafeList<UnsafeList<UnsafeList<Children>>> _thread_size_children;

        public ChildrenPool(int childrenPerThread)
        {
            _nodePool = new(childrenPerThread * 36 * JobsUtility.ThreadIndexCount, Allocator.Persistent);
            _thread_size_children = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);

            // Each thread has its own segment of children, threadSegment
            // Each threadSegment has 8 pools of children organized their size, sizeSegment
            // Each sizeSegment has a childrenPerSize elements, every children has the same number of nodes
            for (int thread = 0, poolIndex = 0; thread < JobsUtility.ThreadIndexCount; thread++)
            {
                UnsafeList<UnsafeList<Children>> threadSegment = new(8, Allocator.Persistent);
                for (int size = 1; size <= 8; size++)
                {
                    UnsafeList<Children> sizeSegment = new(childrenPerThread, Allocator.Persistent);
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
                    threadSegment.AddNoResize(sizeSegment);
                }
                threadSegment.TrimExcess();
                _thread_size_children.AddNoResize(threadSegment);
            }
            _thread_size_children.TrimExcess();

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
            var children = sizeSeg[lastChildren];
            sizeSeg.RemoveAtSwapBack(lastChildren);
            threadSeg[nodeCount - 1] = sizeSeg;
            _thread_size_children[JobsUtility.ThreadIndex] = threadSeg;
            return children;
        }
        public void Release(in Children children)
        {
            children.Reset();
            _thread_size_children[children.Thread][children.Count - 1].AsParallelWriter().AddNoResize(children);
        }

        public void Dispose()
        {
            _thread_size_children.Dispose();
            _nodePool.Dispose();
        }
    }
}