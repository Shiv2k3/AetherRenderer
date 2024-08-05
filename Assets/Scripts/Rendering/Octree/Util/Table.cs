using Unity.Mathematics;

namespace Core.Util
{
    public static class Table
    {
        public static readonly float SQRT3 = 1.73205080757f;

        public static readonly int[] PowersOfTwo = new int[]
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
            1024,  // Depth 10
            2048,  // Depth 11
            4096,  // Depth 12
            8192,  // Depth 13
            16384,  // Depth 14
        };

        public static readonly float[] InvPowersOfTwo = new float[16]
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
            0.0009765625f,  // Depth 10
            0.00048828125f,  // Depth 11
            0.000244140625f,  // Depth 12
            0.0001220703125f,  // Depth 13
            0.00006103515625f,  // Depth 14
            0.00003051757f,     // Depth 15
        };

        public static readonly float[] SubnodeLength = new float[16]
        {
            Settings.WorldSize * 1,    // Depth 0
            Settings.WorldSize * 0.5f,    // Depth 1
            Settings.WorldSize * 0.25f,    // Depth 2
            Settings.WorldSize * 0.125f,    // Depth 3
            Settings.WorldSize * 0.0625f,   // Depth 4
            Settings.WorldSize * 0.03125f,   // Depth 5
            Settings.WorldSize * 0.015625f,   // Depth 6
            Settings.WorldSize * 0.0078125f,  // Depth 7
            Settings.WorldSize * 0.00390625f,  // Depth 8
            Settings.WorldSize * 0.001953125f,  // Depth 9
            Settings.WorldSize * 0.0009765625f,  // Depth 10
            Settings.WorldSize * 0.00048828125f,  // Depth 11
            Settings.WorldSize * 0.000244140625f,  // Depth 12
            Settings.WorldSize * 0.0001220703125f,  // Depth 13
            Settings.WorldSize * 0.00006103515625f,  // Depth 14
            Settings.WorldSize * 0.00003051757f,     // Depth 15
        };

        public static readonly float[] HalfDiagonal = new float[]
        {
            Settings.WorldSize * 0.8660254037844386f,  // Depth 0
            Settings.WorldSize * 0.4330127018922193f,  // Depth 1
            Settings.WorldSize * 0.21650635094610965f, // Depth 2
            Settings.WorldSize * 0.10825317547305482f, // Depth 3
            Settings.WorldSize * 0.05412658773652741f, // Depth 4
            Settings.WorldSize * 0.027063293868263706f,// Depth 5
            Settings.WorldSize * 0.013531646934131853f,// Depth 6
            Settings.WorldSize * 0.0067658234670659265f,// Depth 7
            Settings.WorldSize * 0.0033829117335329633f,// Depth 8
            Settings.WorldSize * 0.0016914558667664816f,// Depth 9
            Settings.WorldSize * 0.0008457279333832408f,// Depth 10
            Settings.WorldSize * 0.0004228639666916204f,// Depth 11
            Settings.WorldSize * 0.0002114319833458102f,// Depth 12
            Settings.WorldSize * 0.0001057159916729051f,// Depth 13
            Settings.WorldSize * 5.285799583645255e-05f,// Depth 14
            Settings.WorldSize * 2.6428997918226276e-05f,// Depth 15
            Settings.WorldSize * 1.3214498959113138e-05f // Depth 16
        };

        public static readonly float3[] UnitCorners = new float3[]
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

        public static readonly float3[][] EdgeTable = new float3[256][]
        {
            new float3[] {},
            new float3[] {},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {},
            new float3[] {},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f)},
            new float3[] {new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
            new float3[] {new(-0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, 0.5f), new(-0.5f, -0.5f, -0.5f), new(-0.5f, -0.5f, 0.5f), new(0.5f, -0.5f, -0.5f), new(0.5f, -0.5f, 0.5f), new(-0.5f, 0.5f, -0.5f), new(-0.5f, 0.5f, 0.5f), new(0.5f, 0.5f, -0.5f), new(0.5f, 0.5f, 0.5f)},
        };
        public static readonly int[][] EdgeIndexTable = new int[256][]
        {
            new int[0],
            new int[0],
            new int[0],
            new int[] {0, 1}, // 00000011
            new int[0],
            new int[] {0, 1}, // 00000101
            new int[0],
            new int[] {0, 1, 0, 2}, // 00000111
            new int[0],
            new int[0],
            new int[] {0, 1}, // 00001010
            new int[] {0, 1, 1, 2}, // 00001011
            new int[] {0, 1}, // 00001100
            new int[] {0, 1, 2, 0}, // 00001101
            new int[] {0, 1, 2, 1}, // 00001110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3}, // 00001111
            new int[0],
            new int[] {0, 1}, // 00010001
            new int[0],
            new int[] {0, 1, 0, 2}, // 00010011
            new int[0],
            new int[] {0, 1, 0, 2}, // 00010101
            new int[0],
            new int[] {0, 1, 0, 2, 0, 3}, // 00010111
            new int[0],
            new int[] {0, 1}, // 00011001
            new int[] {0, 1}, // 00011010
            new int[] {0, 1, 1, 2, 0, 3}, // 00011011
            new int[] {0, 1}, // 00011100
            new int[] {0, 1, 2, 0, 2, 3}, // 00011101
            new int[] {0, 1, 2, 1}, // 00011110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 0, 4}, // 00011111
            new int[0],
            new int[0],
            new int[] {0, 1}, // 00100010
            new int[] {0, 1, 1, 2}, // 00100011
            new int[0],
            new int[] {0, 1}, // 00100101
            new int[] {0, 1}, // 00100110
            new int[] {0, 1, 0, 2, 1, 3}, // 00100111
            new int[0],
            new int[0],
            new int[] {0, 1, 0, 2}, // 00101010
            new int[] {0, 1, 1, 2, 1, 3}, // 00101011
            new int[] {0, 1}, // 00101100
            new int[] {0, 1, 2, 0}, // 00101101
            new int[] {0, 1, 2, 1, 0, 3}, // 00101110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 1, 4}, // 00101111
            new int[] {0, 1}, // 00110000
            new int[] {0, 1, 2, 0}, // 00110001
            new int[] {0, 1, 2, 1}, // 00110010
            new int[] {0, 1, 2, 3, 0, 2, 1, 3}, // 00110011
            new int[] {0, 1}, // 00110100
            new int[] {0, 1, 2, 3, 0, 2}, // 00110101
            new int[] {0, 1, 2, 1}, // 00110110
            new int[] {0, 1, 0, 2, 3, 4, 0, 3, 1, 4}, // 00110111
            new int[] {0, 1}, // 00111000
            new int[] {0, 1, 2, 0}, // 00111001
            new int[] {0, 1, 2, 3, 0, 3}, // 00111010
            new int[] {0, 1, 1, 2, 3, 4, 0, 3, 1, 4}, // 00111011
            new int[] {0, 1, 2, 3}, // 00111100
            new int[] {0, 1, 2, 0, 3, 4, 2, 3}, // 00111101
            new int[] {0, 1, 2, 1, 3, 4, 0, 4}, // 00111110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 0, 4, 1, 5}, // 00111111
            new int[0],
            new int[0],
            new int[0],
            new int[] {0, 1}, // 01000011
            new int[] {0, 1}, // 01000100
            new int[] {0, 1, 1, 2}, // 01000101
            new int[] {0, 1}, // 01000110
            new int[] {0, 1, 0, 2, 2, 3}, // 01000111
            new int[0],
            new int[0],
            new int[] {0, 1}, // 01001010
            new int[] {0, 1, 1, 2}, // 01001011
            new int[] {0, 1, 0, 2}, // 01001100
            new int[] {0, 1, 2, 0, 0, 3}, // 01001101
            new int[] {0, 1, 2, 1, 2, 3}, // 01001110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 3, 4}, // 01001111
            new int[] {0, 1}, // 01010000
            new int[] {0, 1, 2, 0}, // 01010001
            new int[] {0, 1}, // 01010010
            new int[] {0, 1, 2, 3, 0, 2}, // 01010011
            new int[] {0, 1, 2, 1}, // 01010100
            new int[] {0, 1, 2, 3, 0, 2, 1, 3}, // 01010101
            new int[] {0, 1, 2, 1}, // 01010110
            new int[] {0, 1, 0, 2, 3, 4, 0, 3, 2, 4}, // 01010111
            new int[] {0, 1}, // 01011000
            new int[] {0, 1, 2, 0}, // 01011001
            new int[] {0, 1, 2, 3}, // 01011010
            new int[] {0, 1, 1, 2, 3, 4, 0, 3}, // 01011011
            new int[] {0, 1, 2, 3, 0, 3}, // 01011100
            new int[] {0, 1, 2, 0, 3, 4, 2, 3, 0, 4}, // 01011101
            new int[] {0, 1, 2, 1, 3, 4, 2, 4}, // 01011110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 0, 4, 3, 5}, // 01011111
            new int[0],
            new int[0],
            new int[] {0, 1}, // 01100010
            new int[] {0, 1, 1, 2}, // 01100011
            new int[] {0, 1}, // 01100100
            new int[] {0, 1, 1, 2}, // 01100101
            new int[] {0, 1, 2, 3}, // 01100110
            new int[] {0, 1, 0, 2, 1, 3, 2, 4}, // 01100111
            new int[0],
            new int[0],
            new int[] {0, 1, 0, 2}, // 01101010
            new int[] {0, 1, 1, 2, 1, 3}, // 01101011
            new int[] {0, 1, 0, 2}, // 01101100
            new int[] {0, 1, 2, 0, 0, 3}, // 01101101
            new int[] {0, 1, 2, 1, 0, 3, 2, 4}, // 01101110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 1, 4, 3, 5}, // 01101111
            new int[] {0, 1, 0, 2}, // 01110000
            new int[] {0, 1, 0, 2, 3, 0}, // 01110001
            new int[] {0, 1, 0, 2, 3, 1}, // 01110010
            new int[] {0, 1, 2, 3, 2, 4, 0, 2, 1, 3}, // 01110011
            new int[] {0, 1, 0, 2, 3, 2}, // 01110100
            new int[] {0, 1, 2, 3, 2, 4, 0, 2, 1, 4}, // 01110101
            new int[] {0, 1, 0, 2, 3, 1, 4, 2}, // 01110110
            new int[] {0, 1, 0, 2, 3, 4, 3, 5, 0, 3, 1, 4, 2, 5}, // 01110111
            new int[] {0, 1, 0, 2}, // 01111000
            new int[] {0, 1, 0, 2, 3, 0}, // 01111001
            new int[] {0, 1, 2, 3, 2, 4, 0, 3}, // 01111010
            new int[] {0, 1, 1, 2, 3, 4, 3, 5, 0, 3, 1, 4}, // 01111011
            new int[] {0, 1, 2, 3, 2, 4, 0, 4}, // 01111100
            new int[] {0, 1, 2, 0, 3, 4, 3, 5, 2, 3, 0, 5}, // 01111101
            new int[] {0, 1, 2, 1, 3, 4, 3, 5, 0, 4, 2, 5}, // 01111110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 4, 6, 0, 4, 1, 5, 3, 6}, // 01111111
            new int[0],
            new int[0],
            new int[0],
            new int[] {0, 1}, // 10000011
            new int[0],
            new int[] {0, 1}, // 10000101
            new int[0],
            new int[] {0, 1, 0, 2}, // 10000111
            new int[] {0, 1}, // 10001000
            new int[] {0, 1}, // 10001001
            new int[] {0, 1, 1, 2}, // 10001010
            new int[] {0, 1, 1, 2, 2, 3}, // 10001011
            new int[] {0, 1, 1, 2}, // 10001100
            new int[] {0, 1, 2, 0, 1, 3}, // 10001101
            new int[] {0, 1, 2, 1, 1, 3}, // 10001110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 2, 4}, // 10001111
            new int[0],
            new int[] {0, 1}, // 10010001
            new int[0],
            new int[] {0, 1, 0, 2}, // 10010011
            new int[0],
            new int[] {0, 1, 0, 2}, // 10010101
            new int[0],
            new int[] {0, 1, 0, 2, 0, 3}, // 10010111
            new int[] {0, 1}, // 10011000
            new int[] {0, 1, 2, 3}, // 10011001
            new int[] {0, 1, 1, 2}, // 10011010
            new int[] {0, 1, 1, 2, 0, 3, 2, 4}, // 10011011
            new int[] {0, 1, 1, 2}, // 10011100
            new int[] {0, 1, 2, 0, 2, 3, 1, 4}, // 10011101
            new int[] {0, 1, 2, 1, 1, 3}, // 10011110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 0, 4, 2, 5}, // 10011111
            new int[] {0, 1}, // 10100000
            new int[] {0, 1}, // 10100001
            new int[] {0, 1, 2, 0}, // 10100010
            new int[] {0, 1, 2, 3, 1, 2}, // 10100011
            new int[] {0, 1}, // 10100100
            new int[] {0, 1, 2, 3}, // 10100101
            new int[] {0, 1, 2, 0}, // 10100110
            new int[] {0, 1, 0, 2, 3, 4, 1, 3}, // 10100111
            new int[] {0, 1, 2, 1}, // 10101000
            new int[] {0, 1, 2, 1}, // 10101001
            new int[] {0, 1, 2, 3, 0, 2, 1, 3}, // 10101010
            new int[] {0, 1, 1, 2, 3, 4, 1, 3, 2, 4}, // 10101011
            new int[] {0, 1, 2, 3, 1, 3}, // 10101100
            new int[] {0, 1, 2, 0, 3, 4, 1, 4}, // 10101101
            new int[] {0, 1, 2, 1, 3, 4, 0, 3, 1, 4}, // 10101110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 1, 4, 2, 5}, // 10101111
            new int[] {0, 1, 1, 2}, // 10110000
            new int[] {0, 1, 1, 2, 3, 0}, // 10110001
            new int[] {0, 1, 1, 2, 3, 1}, // 10110010
            new int[] {0, 1, 2, 3, 3, 4, 0, 2, 1, 3}, // 10110011
            new int[] {0, 1, 1, 2}, // 10110100
            new int[] {0, 1, 2, 3, 3, 4, 0, 2}, // 10110101
            new int[] {0, 1, 1, 2, 3, 1}, // 10110110
            new int[] {0, 1, 0, 2, 3, 4, 4, 5, 0, 3, 1, 4}, // 10110111
            new int[] {0, 1, 1, 2, 3, 2}, // 10111000
            new int[] {0, 1, 1, 2, 3, 0, 4, 2}, // 10111001
            new int[] {0, 1, 2, 3, 3, 4, 0, 3, 1, 4}, // 10111010
            new int[] {0, 1, 1, 2, 3, 4, 4, 5, 0, 3, 1, 4, 2, 5}, // 10111011
            new int[] {0, 1, 2, 3, 3, 4, 1, 4}, // 10111100
            new int[] {0, 1, 2, 0, 3, 4, 4, 5, 2, 3, 1, 5}, // 10111101
            new int[] {0, 1, 2, 1, 3, 4, 4, 5, 0, 4, 1, 5}, // 10111110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 5, 6, 0, 4, 1, 5, 2, 6}, // 10111111
            new int[] {0, 1}, // 11000000
            new int[] {0, 1}, // 11000001
            new int[] {0, 1}, // 11000010
            new int[] {0, 1, 2, 3}, // 11000011
            new int[] {0, 1, 2, 0}, // 11000100
            new int[] {0, 1, 2, 3, 1, 2}, // 11000101
            new int[] {0, 1, 2, 0}, // 11000110
            new int[] {0, 1, 0, 2, 3, 4, 2, 3}, // 11000111
            new int[] {0, 1, 2, 1}, // 11001000
            new int[] {0, 1, 2, 1}, // 11001001
            new int[] {0, 1, 2, 3, 1, 3}, // 11001010
            new int[] {0, 1, 1, 2, 3, 4, 2, 4}, // 11001011
            new int[] {0, 1, 2, 3, 0, 2, 1, 3}, // 11001100
            new int[] {0, 1, 2, 0, 3, 4, 0, 3, 1, 4}, // 11001101
            new int[] {0, 1, 2, 1, 3, 4, 2, 3, 1, 4}, // 11001110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 3, 4, 2, 5}, // 11001111
            new int[] {0, 1, 2, 0}, // 11010000
            new int[] {0, 1, 2, 0, 3, 2}, // 11010001
            new int[] {0, 1, 2, 0}, // 11010010
            new int[] {0, 1, 2, 3, 4, 2, 0, 4}, // 11010011
            new int[] {0, 1, 2, 0, 3, 0}, // 11010100
            new int[] {0, 1, 2, 3, 4, 2, 0, 4, 1, 2}, // 11010101
            new int[] {0, 1, 2, 0, 3, 0}, // 11010110
            new int[] {0, 1, 0, 2, 3, 4, 5, 3, 0, 5, 2, 3}, // 11010111
            new int[] {0, 1, 2, 0, 3, 1}, // 11011000
            new int[] {0, 1, 2, 0, 3, 2, 4, 1}, // 11011001
            new int[] {0, 1, 2, 3, 4, 2, 1, 3}, // 11011010
            new int[] {0, 1, 1, 2, 3, 4, 5, 3, 0, 5, 2, 4}, // 11011011
            new int[] {0, 1, 2, 3, 4, 2, 0, 2, 1, 3}, // 11011100
            new int[] {0, 1, 2, 0, 3, 4, 5, 3, 2, 5, 0, 3, 1, 4}, // 11011101
            new int[] {0, 1, 2, 1, 3, 4, 5, 3, 2, 3, 1, 4}, // 11011110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 6, 4, 0, 6, 3, 4, 2, 5}, // 11011111
            new int[] {0, 1, 2, 1}, // 11100000
            new int[] {0, 1, 2, 1}, // 11100001
            new int[] {0, 1, 2, 1, 3, 0}, // 11100010
            new int[] {0, 1, 2, 3, 4, 3, 1, 2}, // 11100011
            new int[] {0, 1, 2, 1, 3, 2}, // 11100100
            new int[] {0, 1, 2, 3, 4, 3, 1, 4}, // 11100101
            new int[] {0, 1, 2, 1, 3, 0, 4, 2}, // 11100110
            new int[] {0, 1, 0, 2, 3, 4, 5, 4, 1, 3, 2, 5}, // 11100111
            new int[] {0, 1, 2, 1, 3, 1}, // 11101000
            new int[] {0, 1, 2, 1, 3, 1}, // 11101001
            new int[] {0, 1, 2, 3, 4, 3, 0, 2, 1, 3}, // 11101010
            new int[] {0, 1, 1, 2, 3, 4, 5, 4, 1, 3, 2, 4}, // 11101011
            new int[] {0, 1, 2, 3, 4, 3, 0, 4, 1, 3}, // 11101100
            new int[] {0, 1, 2, 0, 3, 4, 5, 4, 0, 5, 1, 4}, // 11101101
            new int[] {0, 1, 2, 1, 3, 4, 5, 4, 0, 3, 2, 5, 1, 4}, // 11101110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 6, 5, 1, 4, 3, 6, 2, 5}, // 11101111
            new int[] {0, 1, 1, 2, 3, 2, 0, 3}, // 11110000
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 0}, // 11110001
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 1}, // 11110010
            new int[] {0, 1, 2, 3, 3, 4, 5, 4, 2, 5, 0, 2, 1, 3}, // 11110011
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 3}, // 11110100
            new int[] {0, 1, 2, 3, 3, 4, 5, 4, 2, 5, 0, 2, 1, 5}, // 11110101
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 1, 5, 3}, // 11110110
            new int[] {0, 1, 0, 2, 3, 4, 4, 5, 6, 5, 3, 6, 0, 3, 1, 4, 2, 6}, // 11110111
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 2}, // 11111000
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 0, 5, 2}, // 11111001
            new int[] {0, 1, 2, 3, 3, 4, 5, 4, 2, 5, 0, 3, 1, 4}, // 11111010
            new int[] {0, 1, 1, 2, 3, 4, 4, 5, 6, 5, 3, 6, 0, 3, 1, 4, 2, 5}, // 11111011
            new int[] {0, 1, 2, 3, 3, 4, 5, 4, 2, 5, 0, 5, 1, 4}, // 11111100
            new int[] {0, 1, 2, 0, 3, 4, 4, 5, 6, 5, 3, 6, 2, 3, 0, 6, 1, 5}, // 11111101
            new int[] {0, 1, 2, 1, 3, 4, 4, 5, 6, 5, 3, 6, 0, 4, 2, 6, 1, 5}, // 11111110
            new int[] {0, 1, 1, 2, 3, 2, 0, 3, 4, 5, 5, 6, 7, 6, 4, 7, 0, 4, 1, 5, 3, 7, 2, 6}, // 11111111
        };

    }
}