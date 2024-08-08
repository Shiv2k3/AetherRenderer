using Core.Util;

namespace Core.Rendering.Octree
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct Node
    {
        public readonly bool IsLeaf => _map == 0;
        public readonly int SubnodeCount => Unity.Mathematics.math.countbits((int)_map);
        public readonly int VoxelCount => Unity.Mathematics.math.countbits((int)_dataMap);
        public void AssignSubnodes(in int _start, in int _dataStart, in byte _map, byte _dataMap)
        {
            this._map = _map;
            this._dataMap = _dataMap;
            _nodeIndex = _start;
            _dataIndex = _dataStart;
        }

        public byte _map;
        public byte _dataMap;
        public int _nodeIndex;
        public int _dataIndex;

        public readonly ref Node Subnode(int index)
        {
#if DEBUG
            if (index < 0 || index >= SubnodeCount)
                throw new($"The subnode index {index} is outside the range of {SubnodeCount}");
#endif
            return ref SparseOctree.nodePool.Data[_nodeIndex + index];
        }
        public readonly ref Voxel SubnodeVoxel(int index)
        {
#if DEBUG
            if (index < 0 || index >= VoxelCount)
                throw new($"The subnode index {index} is outside the range of {VoxelCount}");
#endif
            return ref SparseOctree.voxelPool.Data[_dataIndex + index];
        }
    }
}