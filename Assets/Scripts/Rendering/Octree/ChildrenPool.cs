using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Core.Octree
{

    internal struct ChildrenPool
    {
        private NativeArray<Node> _nodePool;
        private UnsafeList<NativeList<Children>> _perThreadPerSizePool;

        public ChildrenPool(int childrenPerThread)
        {
            int nodesPerThread = childrenPerThread * 8;
            _nodePool = new(nodesPerThread * JobsUtility.ThreadIndexCount, Allocator.Persistent);
            _perThreadPerSizePool = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);
            for (int thread = 0; thread < JobsUtility.ThreadIndexCount; thread++)
            {
                var threadPool = new NativeList<Children>(childrenPerThread, Allocator.Persistent);
                for (int index = 0; index < childrenPerThread; index++)
                {
                    var nodes = _nodePool.GetSubArray(thread * nodesPerThread + index * 8, 8);
                    var children = new Children(nodes, thread, index);
                    threadPool.AddNoResize(children);
                }
                _perThreadPerSizePool.AddNoResize(threadPool);
            }   
        }

        public Children Get()
        {
            var pool = _perThreadPerSizePool[JobsUtility.ThreadIndex];
            int lastChildren = pool.Length - 1;
            var children = pool[lastChildren];
            pool.RemoveAt(lastChildren);
            return children;
        }
        public void Release(ref Children children) => _perThreadPerSizePool[children.thread].AsParallelWriter().AddNoResize(children);

        public void Dispose()
        {
            _perThreadPerSizePool.Dispose();
            _nodePool.Dispose();
        }
    }
}