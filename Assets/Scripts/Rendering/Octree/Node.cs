namespace Core.Rendering.Octree
{
    public unsafe struct Node
    {
        public ref Node this[int index]
        {
            get
            {
#if DEBUG
                if (isChunkPTR) throw new("Cannot index for children on a node with a chunk pointer");

                int _count = Unity.Mathematics.math.countbits((int)_map);
                if (index > _count) throw new($"The subnode index {index} is out of range {_count}");
#endif
                return ref ((Node*)_ptr)[index];
            }
        }
        public void AssignChunk(in int chunkIndex, in Voxel* chunk)
        {
#if DEBUG
            if (chunkIndex < 0 || chunkIndex > 255) throw new($"The chunk index {chunkIndex} is out of range");
            isChunkPTR = true;
#endif
            _map = (byte)chunkIndex;
            _ptr = chunk;
        }
        private void* _ptr;
        private byte _map;
#if DEBUG
        private bool isChunkPTR;
#endif
    }
}