using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Core.Rendering.Octree
{
    public unsafe struct Pool
    {
        private NativeList<NativeArray<Voxel>> chunks;
        private UnsafeAtomicCounter32 chunksCount;

        public Pool(in int totalNodes)
        {
            throw new System.NotImplementedException();
        }

        /// <param name="_count">The number of memory aligned nodes to lease</param>
        /// <returns>The pointer to first node of the lease</returns>
        public Node* Lease(in int _count)
        {
            throw new System.NotImplementedException();
        }

        /// <param name="start">The pointer to the first node of the lease</param>
        /// <param name="count">The number of memory aligned nodes leased</param>
        public void Release(in Node* start, in int count)
        {
            throw new System.NotImplementedException();
        }

        /// <param name="chunkIndex">The index of the leased chunk</param>
        /// <param name="chunk">The pointer to the first voxel of the lease</param>
        public void Lease(out int chunkIndex, out Voxel* chunk)
        {
            var pw = chunks.AsParallelWriter();
            chunkIndex = *chunksCount.Counter;
            chunk = (Voxel*)chunks[chunkIndex].GetUnsafePtr();
            chunks.RemoveAt(chunkIndex);
            chunksCount.Sub(1);
        }
    }
}