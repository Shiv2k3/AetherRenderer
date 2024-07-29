using Unity.Burst;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Core.Util
{
    [BurstCompile(OptimizeFor = OptimizeFor.Performance, FloatPrecision = FloatPrecision.Low, FloatMode = FloatMode.Fast)]

    public class SDFs
    {
        public static float SDLine(float3 p, float3 a, float3 b, float radius)
        {
            float3 pa = p - a, ba = b - a;
            float h = clamp(dot(pa, ba) / dot(ba, ba), 0, 1);
            return length(pa - ba * h) - radius;
        }
        public static float SDSphere(in float3 p, in float radius, out float3 gradient)
        {
            float leng = length(p);
            gradient = p / leng;
            return leng - radius;
        }
        public static float SDBoxFrame(float3 p, float3 b, float e)
        {
            p = abs(p) - b;
            float3 q = abs(p + e) - e;
            return min(min(
                length(max(float3(p.x, q.y, q.z), 0)) + min(max(p.x, max(q.y, q.z)), 0),
                length(max(float3(q.x, p.y, q.z), 0)) + min(max(q.x, max(p.y, q.z)), 0)),
                length(max(float3(q.x, q.y, p.z), 0)) + min(max(q.x, max(q.y, p.z)), 0));
        }
        public static float SDBox(in float3 p, in float3 b)
        {
            float3 q = abs(p) - b;
            return length(max(q, 0)) + min(max(q.x, max(q.y, q.z)), 0);
        }
        public static float SDCylinder(float3 p, float h, float r)
        {
            float2 d = abs(float2(length(p.xz), p.y)) - float2(r, h);
            return min(max(d.x, d.y), 0) + length(max(d, 0));
        }

        public static float Merge(in float d1, in float d2) { return min(d1, d2); }
        public static float Intersect(in float d1, in float d2) { return max(d1, d2); }
        /// <summary>
        /// Subtracts one distance field from the other
        /// </summary>
        /// <param name="d1"> The field doing the subtracting </param> 
        /// <param name="d2"> The field getting subtracted </param> 
        /// <returns> The subtracted field </returns>
        public static float Subtract(in float d1, in float d2) { return max(-d1, d2); }
    }
}