using Unity.Collections;

namespace Core.Octree
{
    public struct Children
    {
        public NativeArray<Node> Nodes;
        public byte Thread { get; private set; }
        public bool IsEmpty { get; set; }

        public Children(in NativeArray<Node> nodes, byte thread)
        {
            Nodes = nodes;
            Thread = thread;
            IsEmpty = true;
        }
        public void Reset()
        {
            IsEmpty = true;
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = Node.Invalid;
            }
        }
        public static Children Empty => new() { IsEmpty = true };
    }
}