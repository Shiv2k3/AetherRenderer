using Unity.Burst;
using Unity.Jobs;

namespace Core.Rendering.Octree
{
    [BurstCompile]
    public struct DualContourer : IJobParallelFor
    {
        private readonly Node root;

        private DualContourer(in Node root)
        {
            this.root = root;
        }
        public void Execute(int index)
        {
            // Recursivly collect the leaf nodes
            // Calculate the feature point of each leaf node
            // Connect feature points
        }
    }
}
