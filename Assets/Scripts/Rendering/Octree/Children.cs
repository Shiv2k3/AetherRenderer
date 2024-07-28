using Unity.Collections;

namespace Core.Octree
{
    public struct Children
    {
        public NativeArray<Node> nodes;
        public int thread;
        public int index;

        public Children(in NativeArray<Node> nodes, int thread, int index)
        {
            this.nodes = nodes;
            this.thread = thread;
            this.index = index;
        }

        public static Children Empty => new() { thread = -1 };
        public readonly bool IsEmpty => thread == -1;
    }
}