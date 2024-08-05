using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Core.Util
{
    public readonly struct Voxel
    {
        private readonly byte _density;
        public readonly float Density
        {
            get
            {
                return (_density / 255f - 0.5f) * 2f;
            }
        }

        private readonly byte _material;
        public readonly uint Material
        {
            get
            {
                return _material;
            }
        }

        private readonly byte _theta;
        private readonly byte _phi;
        public float3 Normal
        {
            get
            {
                sincos(remap(0, byte.MaxValue, -PI, PI, _theta), out float sinTheta, out float cosTheta);
                sincos(remap(0, byte.MaxValue, 0, PI, _phi), out float sinPhi, out float cosPhi);
                return float3(cosTheta * sinPhi, cosPhi, sinTheta * sinPhi);
            }
        }

        public Voxel(float Distance, uint Material, float3 Gradient)
        {
            _density = (byte)((clamp(Distance, -1f, 1f) / 2f + 0.5f) * byte.MaxValue);
            _material = (byte)Material;

            var s = PolarCoordinates.GetPolar(Gradient);
            _theta = (byte)remap(-PI, PI, 0, byte.MaxValue, s.Radial);
            _phi = (byte)remap(0, PI, 0, byte.MaxValue, s.Inclination);
        }
        public Voxel(in HermiteData data) : this(data.Distance, data.Material, data.Gradient) { }
    }
}