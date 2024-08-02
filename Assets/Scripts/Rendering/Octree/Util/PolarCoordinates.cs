using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Core.Util
{
    public struct PolarCoordinates
    {
        public float2 v;
        public float Radial { get => v.x; set => v.x = value; }
        public float Inclination { get => v.y; set => v.y = value; }
        public PolarCoordinates(float radial, float inclination)
        {
            v = float2(radial, inclination);
            Radial = radial;
            Inclination = inclination;
        }
    }
}