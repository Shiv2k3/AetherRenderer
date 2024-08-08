using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Core.Rendering.Octree
{
    public unsafe struct Pool<T> where T : unmanaged
    {
        public readonly ref T this[int index]
        {
            get
            {
                CheckMemoryAccessAndThrow(index, "indexing");
                return ref _pool[index];
            }
        }

        public readonly int nodesPerThread;
        private readonly NativeArray<Node> pool;
        public NativeArray<int> lengths;
        private T* _pool;

        public Pool(in int allocationDepth)
        {
            int totalNodes = ((int)math.pow(8, allocationDepth + 1) - 1) / 7;
            pool = new(totalNodes, Allocator.Persistent, NativeArrayOptions.ClearMemory);
            lengths = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);

            nodesPerThread = totalNodes / JobsUtility.ThreadIndexCount;
            _pool = (T*)pool.GetUnsafePtr();

            for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
            {
                lengths[i] = nodesPerThread;
            }
        }
        public void Dispose()
        {
            pool.Dispose();
            lengths.Dispose();
            _pool = null;

            UnityEngine.Debug.Log("Disposed Pool<" + typeof(T) + ">");
        }

        /// <param name="_count">The number of memory aligned nodes to lease</param>
        /// <param name="startIndex">The index of T in the pool</param>
        public void Lease(in int _count, out int start) => start = Get(_count);

        /// <param name="_count">The number of memory aligned nodes to lease</param>
        /// <returns>The pointer to the start of the leased memory</returns>
        public T* Lease(in int _count)
        {
            int start = Get(_count);
            return _pool + start;
        }

        /// <param name="_count">The number of memory aligned nodes to lease</param>
        /// <param name="value">The T leased from the pool</param>
        /// <returns>The index of T in the pool</returns>
        public int Lease(in int _count, out T* value)
        {
            int start = Get(_count);
            value = _pool + start;
            return start;
        }

        private int Get(int _count)
        {
#if DEBUG
            if (_count < 1 || _count > 8)
                throw new($"{_count} is out of leaseable range");
#endif

            int threadIndex = JobsUtility.ThreadIndex;
            int start = threadIndex * nodesPerThread;

            // Calculate the potential start index
            int potentialStart = start + lengths[threadIndex] - _count;

            // Check memory access before actually returning the start index
            CheckMemoryAccessAndThrow(potentialStart, "leasing");

            // Update the lengths array for the current thread
            lengths[threadIndex] -= _count;

            return potentialStart;
        }
        public readonly ref T GetWithoutChecks(in int index) => ref *(_pool + index);


        [Conditional("DEBUG")]
        private readonly void CheckMemoryAccessAndThrow(int index, string action)
        {
            if (index < JobsUtility.ThreadIndex * nodesPerThread)
                throw new($"Thread# {JobsUtility.ThreadIndex} tried to access memory at {JobsUtility.ThreadIndex * nodesPerThread - index} when {action} in Pool<{typeof(T)}>");

            if (index >= (JobsUtility.ThreadIndex + 1) * nodesPerThread)
                throw new($"Thread# {JobsUtility.ThreadIndex} tried to access memory at {(JobsUtility.ThreadIndex + 1) * nodesPerThread - index} when {action} in Pool<{typeof(T)}>");
        }
    }
}