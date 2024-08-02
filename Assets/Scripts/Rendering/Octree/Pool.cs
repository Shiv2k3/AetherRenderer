using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Core.Rendering.Octree
{
    public unsafe struct Pool
    {
        private const int VoxelsPerChunk = 4096;
        private const int TotalChunks = 256;
        private readonly int nodesPerThread;

        private readonly NativeArray<Voxel> chunks;
        private NativeArray<int> lastChunkIndex;

        private readonly NativeArray<Node> pool;
        public NativeArray<int> lengths;
        public Pool(in int allocationDepth)
        {
            int totalVoxels = VoxelsPerChunk * TotalChunks;
            chunks = new(totalVoxels, Allocator.Persistent);
            lastChunkIndex = new(1, Allocator.Persistent);
            lastChunkIndex[0] = totalVoxels - VoxelsPerChunk - 1;

            int totalNodes = ((int)math.pow(8, allocationDepth + 1) - 1) / 7;
            pool = new(totalNodes, Allocator.Persistent, NativeArrayOptions.ClearMemory);
            lengths = new(JobsUtility.ThreadIndexCount, Allocator.Persistent);
            nodesPerThread = totalNodes / JobsUtility.ThreadIndexCount;
            for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
            {
                lengths[i] = nodesPerThread;
            }
        }
        public readonly void Dispose()
        {
            chunks.Dispose();
            lastChunkIndex.Dispose();
            pool.Dispose();
            lengths.Dispose();
            UnityEngine.Debug.Log("Disposed Pool");
        }

        /// <param name="_count">The number of memory aligned nodes to lease</param>
        /// <returns>The pointer to first node of the lease</returns>
        public Node* Lease(in int _count)
        {
            int segmentStart = JobsUtility.ThreadIndex * nodesPerThread;
            int nodeIndex = segmentStart + lengths[JobsUtility.ThreadIndex] - _count;
            lengths[JobsUtility.ThreadIndex] -= _count;
#if DEBUG
            if (nodeIndex < 0) throw new($"Thread segment# {JobsUtility.ThreadIndex} is out of nodes");
#endif
            return (Node*)pool.GetUnsafePtr() + nodeIndex;
        }

        /// <param name="chunkIndex">The index of the leased chunk</param>
        /// <param name="chunk">The pointer to the first voxel of the lease</param>
        public void Lease(out int chunkIndex, out Voxel* chunk)
        {
            chunkIndex = lastChunkIndex[0];
            chunk = (Voxel*)chunks.GetUnsafePtr() + chunkIndex;
            lastChunkIndex[0] -= VoxelsPerChunk;
            chunkIndex /= VoxelsPerChunk;
        }
    }
}