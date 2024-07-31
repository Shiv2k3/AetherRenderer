using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Core.Octree
{
    public unsafe struct Children
    {
        public Node* Nodes;
        public ref Node this[int index] => ref Nodes[index];
        public readonly int Count => (_data & _countMask) + 1;
        public readonly int Thread => (_data & _threadMask) >> 3;
        public bool IsEmpty { get; set; }

        private readonly byte _data;
        private const byte _countMask = 0b0000_0111;
        private const byte _threadMask = 0b1111_1000;

        public Children(in NativeArray<Node> nodes, byte thread)
        {
            Nodes = (Node*)nodes.GetUnsafePtr();
            _data = (byte)((nodes.Length - 1) & _countMask);
            _data |= (byte)((thread << 3) & _threadMask);
            IsEmpty = true;
            if (Count != nodes.Length) throw new($"Expected Count value {nodes.Length}, got {Count}");
            if (Thread != thread) throw new($"Expected thread value {thread}, got {Thread}");
        }
        public static Children Empty => new() { IsEmpty = true };
    }
}