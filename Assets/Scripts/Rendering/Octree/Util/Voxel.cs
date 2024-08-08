using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Core.Util
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly struct Voxel
    {
        public static readonly int LatticeResolution = 512;
        public static readonly int TotalLatticeNodes = 134217728;
        public const int ByteSize = 12 + 1 + 2;

        public readonly float3 Position;
        public readonly byte Material;
        public float3 Normal
        {
            get
            {
                sincos(remap(0, byte.MaxValue, -PI, PI, _theta), out float sinTheta, out float cosTheta);
                sincos(remap(0, byte.MaxValue, 0, PI, _phi), out float sinPhi, out float cosPhi);
                return float3(cosTheta * sinPhi, cosPhi, sinTheta * sinPhi);
            }
        }

        private readonly byte _theta;
        private readonly byte _phi;
        public Voxel(in float3 Position, in byte Material, in float3 Gradient)
        {
            this.Position = Position;
            this.Material = Material;

            var s = PolarCoordinates.GetPolar(Gradient);
            _theta = (byte)remap(-PI, PI, 0, byte.MaxValue, s.Radial);
            _phi = (byte)remap(0, PI, 0, byte.MaxValue, s.Inclination);
        }
        public int LatticeIndex
        {
            get
            {
                float3 inBound = Position / Settings.HalfWorldSize;
                int index = IndexPosition.CellIndex(inBound, LatticeResolution);
                return index;
            }
        }

        public override int GetHashCode()
        {
            return LatticeIndex;
        }
    }
}