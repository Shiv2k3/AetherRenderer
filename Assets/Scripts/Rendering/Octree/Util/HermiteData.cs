using Unity.Mathematics;

namespace Core.Util
{
    public struct HermiteData
    {
        public float Distance;
        public float3 Gradient;
        public uint Material;

        public HermiteData(float distance, uint material, float3 gradient)
        {
            Distance = distance;
            Material = material;
            Gradient = gradient;
        }
    }
}
