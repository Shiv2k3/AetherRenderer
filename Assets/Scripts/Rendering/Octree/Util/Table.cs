using Unity.Burst;
using Unity.Mathematics;

namespace Core.Util
{
    public static class Table
    {
        public static float SQRT3 = 1.73205080757f;

        public static readonly float[] PowersOfTwo = new float[]
        {
            1,    // Depth 0
            2,    // Depth 1
            4,    // Depth 2
            8,    // Depth 3
            16,   // Depth 4
            32,   // Depth 5
            64,   // Depth 6
            128,  // Depth 7
            256,  // Depth 8
            512,  // Depth 9
            1024  // Depth 10
        };

        public static readonly float[] InvPowersOfTwo = new float[]
        {
            1,    // Depth 0
            0.5f,    // Depth 1
            0.25f,    // Depth 2
            0.125f,    // Depth 3
            0.0625f,   // Depth 4
            0.03125f,   // Depth 5
            0.015625f,   // Depth 6
            0.0078125f,  // Depth 7
            0.00390625f,  // Depth 8
            0.001953125f,  // Depth 9
            0.0009765625f  // Depth 10
        };

        public static readonly float[] OctantHalfDiagonal = new float[]
        {
            0.8660254037844386f, // Depth 0
            0.4330127018922193f, // Depth 1
            0.21650635094610965f, // Depth 2
            0.10825317547305482f, // Depth 3
            0.05412658773652741f, // Depth 4
            0.027063293868263706f, // Depth 5
            0.013531646934131853f, // Depth 6
            0.0067658234670659265f, // Depth 7
            0.0033829117335329633f, // Depth 8
            0.0016914558667664816f, // Depth 9
            0.0008457279333832408f  // Depth 10
        };

        public static readonly float3[] LocalOctantPosition = new float3[]
        {
            new(-0.5f, -0.5f, -0.5f), // Bottom-left-back
            new( 0.5f, -0.5f, -0.5f), // Bottom-right-back
            new(-0.5f,  0.5f, -0.5f), // Top-left-back
            new( 0.5f,  0.5f, -0.5f), // Top-right-back
            new(-0.5f, -0.5f,  0.5f), // Bottom-left-front
            new( 0.5f, -0.5f,  0.5f), // Bottom-right-front
            new(-0.5f,  0.5f,  0.5f), // Top-left-front
            new( 0.5f,  0.5f,  0.5f)  // Top-right-front

        };
    }
}