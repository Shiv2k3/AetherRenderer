﻿namespace Core.Rendering.Octree
{
    public unsafe struct Node
    {
        public ref Node this[int index]
        {
            get
            {
#if DEBUG
                if (isChunkPtr) throw new("Cannot index for children on a node with a chunk pointer");
                int _count = Unity.Mathematics.math.countbits((int)_map);
                if (index > _count) throw new($"The subnode index {index} is out of range {_count}");
#endif
                return ref ((Node*)_ptr)[index];
            }
        }
        public readonly bool IsLeaf => _map == 0;
        public readonly int Count => Unity.Mathematics.math.countbits((int)_map);

        public void AssignChunk(in int chunkIndex, in Voxel* chunk)
        {
#if DEBUG
            if (chunkIndex < 0 || chunkIndex > 255) throw new($"The chunk index {chunkIndex} is out of range");
            isChunkPtr = true;
#endif
            _map = (byte)chunkIndex;
            _ptr = chunk;
        }
        public void AssignSubnodes(in Node* start, in byte map)
        {
#if DEBUG
            if (isChunkPtr) throw new("Node is already assigned to a chunk");
#endif
            _ptr = start;
            _map = map;
        }

        private void* _ptr;
        public byte _map;
        public Voxel data;
#if DEBUG
        public bool isChunkPtr;
#endif
    }
}