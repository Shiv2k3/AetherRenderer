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
        public static readonly float[] HalfSubnodeLength = new float[16]
        {
            Settings.HalfWorldSize,                    // Depth 0
            Settings.HalfWorldSize * 0.5f,             // Depth 1
            Settings.HalfWorldSize * 0.25f,            // Depth 2
            Settings.HalfWorldSize * 0.125f,           // Depth 3
            Settings.HalfWorldSize * 0.0625f,          // Depth 4
            Settings.HalfWorldSize * 0.03125f,         // Depth 5
            Settings.HalfWorldSize * 0.015625f,        // Depth 6
            Settings.HalfWorldSize * 0.0078125f,       // Depth 7
            Settings.HalfWorldSize * 0.00390625f,      // Depth 8
            Settings.HalfWorldSize * 0.001953125f,     // Depth 9
            Settings.HalfWorldSize * 0.0009765625f,    // Depth 10
            Settings.HalfWorldSize * 0.00048828125f,   // Depth 11
            Settings.HalfWorldSize * 0.000244140625f,  // Depth 12
            Settings.HalfWorldSize * 0.0001220703125f, // Depth 13
            Settings.HalfWorldSize * 0.00006103515625f, // Depth 14
            Settings.HalfWorldSize * 0.000030517578125f // Depth 15
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

        public static readonly float3[] CubeCorners = new float3[]
        {
            new float3(-0.5f, -0.5f, -0.5f), // Bottom-left-back
            new float3( 0.5f, -0.5f, -0.5f), // Bottom-right-back
            new float3(-0.5f,  0.5f, -0.5f), // Top-left-back
            new float3( 0.5f,  0.5f, -0.5f), // Top-right-back
            new float3(-0.5f, -0.5f,  0.5f), // Bottom-left-front
            new float3( 0.5f, -0.5f,  0.5f), // Bottom-right-front
            new float3(-0.5f,  0.5f,  0.5f), // Top-left-front
            new float3( 0.5f,  0.5f,  0.5f)  // Top-right-front
        };

        private static readonly int Top = Voxel.LatticeResolution * 2;
        private static readonly int Bottom = -Voxel.LatticeResolution * 2;
        private static readonly int Left = -1;
        private static readonly int Right = 1;
        private static readonly int Front = Voxel.LatticeResolution * Voxel.LatticeResolution;
        private static readonly int Back = -Voxel.LatticeResolution * Voxel.LatticeResolution;

        public static readonly int[][] IndexMovement = new int[][]
        {
            new int[]
            {
                (int)(Back * HalfSubnodeLength[0] + Left * HalfSubnodeLength[0] + Bottom * HalfSubnodeLength[0]),
                (int)(Back * HalfSubnodeLength[0] + Right * HalfSubnodeLength[0] + Bottom * HalfSubnodeLength[0]),
                (int)(Back * HalfSubnodeLength[0] + Left * HalfSubnodeLength[0] + Top * HalfSubnodeLength[0]),
                (int)(Back * HalfSubnodeLength[0] + Right * HalfSubnodeLength[0] + Top * HalfSubnodeLength[0]),
                (int)(Front * HalfSubnodeLength[0] + Left * HalfSubnodeLength[0] + Bottom * HalfSubnodeLength[0]),
                (int)(Front * HalfSubnodeLength[0] + Right * HalfSubnodeLength[0] + Bottom * HalfSubnodeLength[0]),
                (int)(Front * HalfSubnodeLength[0] + Left * HalfSubnodeLength[0] + Top * HalfSubnodeLength[0]),
                (int)(Front * HalfSubnodeLength[0] + Right * HalfSubnodeLength[0] + Top * HalfSubnodeLength[0])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[1] + Left * HalfSubnodeLength[1] + Bottom * HalfSubnodeLength[1]),
                (int)(Back * HalfSubnodeLength[1] + Right * HalfSubnodeLength[1] + Bottom * HalfSubnodeLength[1]),
                (int)(Back * HalfSubnodeLength[1] + Left * HalfSubnodeLength[1] + Top * HalfSubnodeLength[1]),
                (int)(Back * HalfSubnodeLength[1] + Right * HalfSubnodeLength[1] + Top * HalfSubnodeLength[1]),
                (int)(Front * HalfSubnodeLength[1] + Left * HalfSubnodeLength[1] + Bottom * HalfSubnodeLength[1]),
                (int)(Front * HalfSubnodeLength[1] + Right * HalfSubnodeLength[1] + Bottom * HalfSubnodeLength[1]),
                (int)(Front * HalfSubnodeLength[1] + Left * HalfSubnodeLength[1] + Top * HalfSubnodeLength[1]),
                (int)(Front * HalfSubnodeLength[1] + Right * HalfSubnodeLength[1] + Top * HalfSubnodeLength[1])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[2] + Left * HalfSubnodeLength[2] + Bottom * HalfSubnodeLength[2]),
                (int)(Back * HalfSubnodeLength[2] + Right * HalfSubnodeLength[2] + Bottom * HalfSubnodeLength[2]),
                (int)(Back * HalfSubnodeLength[2] + Left * HalfSubnodeLength[2] + Top * HalfSubnodeLength[2]),
                (int)(Back * HalfSubnodeLength[2] + Right * HalfSubnodeLength[2] + Top * HalfSubnodeLength[2]),
                (int)(Front * HalfSubnodeLength[2] + Left * HalfSubnodeLength[2] + Bottom * HalfSubnodeLength[2]),
                (int)(Front * HalfSubnodeLength[2] + Right * HalfSubnodeLength[2] + Bottom * HalfSubnodeLength[2]),
                (int)(Front * HalfSubnodeLength[2] + Left * HalfSubnodeLength[2] + Top * HalfSubnodeLength[2]),
                (int)(Front * HalfSubnodeLength[2] + Right * HalfSubnodeLength[2] + Top * HalfSubnodeLength[2])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[3] + Left * HalfSubnodeLength[3] + Bottom * HalfSubnodeLength[3]),
                (int)(Back * HalfSubnodeLength[3] + Right * HalfSubnodeLength[3] + Bottom * HalfSubnodeLength[3]),
                (int)(Back * HalfSubnodeLength[3] + Left * HalfSubnodeLength[3] + Top * HalfSubnodeLength[3]),
                (int)(Back * HalfSubnodeLength[3] + Right * HalfSubnodeLength[3] + Top * HalfSubnodeLength[3]),
                (int)(Front * HalfSubnodeLength[3] + Left * HalfSubnodeLength[3] + Bottom * HalfSubnodeLength[3]),
                (int)(Front * HalfSubnodeLength[3] + Right * HalfSubnodeLength[3] + Bottom * HalfSubnodeLength[3]),
                (int)(Front * HalfSubnodeLength[3] + Left * HalfSubnodeLength[3] + Top * HalfSubnodeLength[3]),
                (int)(Front * HalfSubnodeLength[3] + Right * HalfSubnodeLength[3] + Top * HalfSubnodeLength[3])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[4] + Left * HalfSubnodeLength[4] + Bottom * HalfSubnodeLength[4]),
                (int)(Back * HalfSubnodeLength[4] + Right * HalfSubnodeLength[4] + Bottom * HalfSubnodeLength[4]),
                (int)(Back * HalfSubnodeLength[4] + Left * HalfSubnodeLength[4] + Top * HalfSubnodeLength[4]),
                (int)(Back * HalfSubnodeLength[4] + Right * HalfSubnodeLength[4] + Top * HalfSubnodeLength[4]),
                (int)(Front * HalfSubnodeLength[4] + Left * HalfSubnodeLength[4] + Bottom * HalfSubnodeLength[4]),
                (int)(Front * HalfSubnodeLength[4] + Right * HalfSubnodeLength[4] + Bottom * HalfSubnodeLength[4]),
                (int)(Front * HalfSubnodeLength[4] + Left * HalfSubnodeLength[4] + Top * HalfSubnodeLength[4]),
                (int)(Front * HalfSubnodeLength[4] + Right * HalfSubnodeLength[4] + Top * HalfSubnodeLength[4])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[5] + Left * HalfSubnodeLength[5] + Bottom * HalfSubnodeLength[5]),
                (int)(Back * HalfSubnodeLength[5] + Right * HalfSubnodeLength[5] + Bottom * HalfSubnodeLength[5]),
                (int)(Back * HalfSubnodeLength[5] + Left * HalfSubnodeLength[5] + Top * HalfSubnodeLength[5]),
                (int)(Back * HalfSubnodeLength[5] + Right * HalfSubnodeLength[5] + Top * HalfSubnodeLength[5]),
                (int)(Front * HalfSubnodeLength[5] + Left * HalfSubnodeLength[5] + Bottom * HalfSubnodeLength[5]),
                (int)(Front * HalfSubnodeLength[5] + Right * HalfSubnodeLength[5] + Bottom * HalfSubnodeLength[5]),
                (int)(Front * HalfSubnodeLength[5] + Left * HalfSubnodeLength[5] + Top * HalfSubnodeLength[5]),
                (int)(Front * HalfSubnodeLength[5] + Right * HalfSubnodeLength[5] + Top * HalfSubnodeLength[5])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[6] + Left * HalfSubnodeLength[6] + Bottom * HalfSubnodeLength[6]),
                (int)(Back * HalfSubnodeLength[6] + Right * HalfSubnodeLength[6] + Bottom * HalfSubnodeLength[6]),
                (int)(Back * HalfSubnodeLength[6] + Left * HalfSubnodeLength[6] + Top * HalfSubnodeLength[6]),
                (int)(Back * HalfSubnodeLength[6] + Right * HalfSubnodeLength[6] + Top * HalfSubnodeLength[6]),
                (int)(Front * HalfSubnodeLength[6] + Left * HalfSubnodeLength[6] + Bottom * HalfSubnodeLength[6]),
                (int)(Front * HalfSubnodeLength[6] + Right * HalfSubnodeLength[6] + Bottom * HalfSubnodeLength[6]),
                (int)(Front * HalfSubnodeLength[6] + Left * HalfSubnodeLength[6] + Top * HalfSubnodeLength[6]),
                (int)(Front * HalfSubnodeLength[6] + Right * HalfSubnodeLength[6] + Top * HalfSubnodeLength[6])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[7] + Left * HalfSubnodeLength[7] + Bottom * HalfSubnodeLength[7]),
                (int)(Back * HalfSubnodeLength[7] + Right * HalfSubnodeLength[7] + Bottom * HalfSubnodeLength[7]),
                (int)(Back * HalfSubnodeLength[7] + Left * HalfSubnodeLength[7] + Top * HalfSubnodeLength[7]),
                (int)(Back * HalfSubnodeLength[7] + Right * HalfSubnodeLength[7] + Top * HalfSubnodeLength[7]),
                (int)(Front * HalfSubnodeLength[7] + Left * HalfSubnodeLength[7] + Bottom * HalfSubnodeLength[7]),
                (int)(Front * HalfSubnodeLength[7] + Right * HalfSubnodeLength[7] + Bottom * HalfSubnodeLength[7]),
                (int)(Front * HalfSubnodeLength[7] + Left * HalfSubnodeLength[7] + Top * HalfSubnodeLength[7]),
                (int)(Front * HalfSubnodeLength[7] + Right * HalfSubnodeLength[7] + Top * HalfSubnodeLength[7])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[8] + Left * HalfSubnodeLength[8] + Bottom * HalfSubnodeLength[8]),
                (int)(Back * HalfSubnodeLength[8] + Right * HalfSubnodeLength[8] + Bottom * HalfSubnodeLength[8]),
                (int)(Back * HalfSubnodeLength[8] + Left * HalfSubnodeLength[8] + Top * HalfSubnodeLength[8]),
                (int)(Back * HalfSubnodeLength[8] + Right * HalfSubnodeLength[8] + Top * HalfSubnodeLength[8]),
                (int)(Front * HalfSubnodeLength[8] + Left * HalfSubnodeLength[8] + Bottom * HalfSubnodeLength[8]),
                (int)(Front * HalfSubnodeLength[8] + Right * HalfSubnodeLength[8] + Bottom * HalfSubnodeLength[8]),
                (int)(Front * HalfSubnodeLength[8] + Left * HalfSubnodeLength[8] + Top * HalfSubnodeLength[8]),
                (int)(Front * HalfSubnodeLength[8] + Right * HalfSubnodeLength[8] + Top * HalfSubnodeLength[8])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[9] + Left * HalfSubnodeLength[9] + Bottom * HalfSubnodeLength[9]),
                (int)(Back * HalfSubnodeLength[9] + Right * HalfSubnodeLength[9] + Bottom * HalfSubnodeLength[9]),
                (int)(Back * HalfSubnodeLength[9] + Left * HalfSubnodeLength[9] + Top * HalfSubnodeLength[9]),
                (int)(Back * HalfSubnodeLength[9] + Right * HalfSubnodeLength[9] + Top * HalfSubnodeLength[9]),
                (int)(Front * HalfSubnodeLength[9] + Left * HalfSubnodeLength[9] + Bottom * HalfSubnodeLength[9]),
                (int)(Front * HalfSubnodeLength[9] + Right * HalfSubnodeLength[9] + Bottom * HalfSubnodeLength[9]),
                (int)(Front * HalfSubnodeLength[9] + Left * HalfSubnodeLength[9] + Top * HalfSubnodeLength[9]),
                (int)(Front * HalfSubnodeLength[9] + Right * HalfSubnodeLength[9] + Top * HalfSubnodeLength[9])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[10] + Left * HalfSubnodeLength[10] + Bottom * HalfSubnodeLength[10]),
                (int)(Back * HalfSubnodeLength[10] + Right * HalfSubnodeLength[10] + Bottom * HalfSubnodeLength[10]),
                (int)(Back * HalfSubnodeLength[10] + Left * HalfSubnodeLength[10] + Top * HalfSubnodeLength[10]),
                (int)(Back * HalfSubnodeLength[10] + Right * HalfSubnodeLength[10] + Top * HalfSubnodeLength[10]),
                (int)(Front * HalfSubnodeLength[10] + Left * HalfSubnodeLength[10] + Bottom * HalfSubnodeLength[10]),
                (int)(Front * HalfSubnodeLength[10] + Right * HalfSubnodeLength[10] + Bottom * HalfSubnodeLength[10]),
                (int)(Front * HalfSubnodeLength[10] + Left * HalfSubnodeLength[10] + Top * HalfSubnodeLength[10]),
                (int)(Front * HalfSubnodeLength[10] + Right * HalfSubnodeLength[10] + Top * HalfSubnodeLength[10])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[11] + Left * HalfSubnodeLength[11] + Bottom * HalfSubnodeLength[11]),
                (int)(Back * HalfSubnodeLength[11] + Right * HalfSubnodeLength[11] + Bottom * HalfSubnodeLength[11]),
                (int)(Back * HalfSubnodeLength[11] + Left * HalfSubnodeLength[11] + Top * HalfSubnodeLength[11]),
                (int)(Back * HalfSubnodeLength[11] + Right * HalfSubnodeLength[11] + Top * HalfSubnodeLength[11]),
                (int)(Front * HalfSubnodeLength[11] + Left * HalfSubnodeLength[11] + Bottom * HalfSubnodeLength[11]),
                (int)(Front * HalfSubnodeLength[11] + Right * HalfSubnodeLength[11] + Bottom * HalfSubnodeLength[11]),
                (int)(Front * HalfSubnodeLength[11] + Left * HalfSubnodeLength[11] + Top * HalfSubnodeLength[11]),
                (int)(Front * HalfSubnodeLength[11] + Right * HalfSubnodeLength[11] + Top * HalfSubnodeLength[11])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[12] + Left * HalfSubnodeLength[12] + Bottom * HalfSubnodeLength[12]),
                (int)(Back * HalfSubnodeLength[12] + Right * HalfSubnodeLength[12] + Bottom * HalfSubnodeLength[12]),
                (int)(Back * HalfSubnodeLength[12] + Left * HalfSubnodeLength[12] + Top * HalfSubnodeLength[12]),
                (int)(Back * HalfSubnodeLength[12] + Right * HalfSubnodeLength[12] + Top * HalfSubnodeLength[12]),
                (int)(Front * HalfSubnodeLength[12] + Left * HalfSubnodeLength[12] + Bottom * HalfSubnodeLength[12]),
                (int)(Front * HalfSubnodeLength[12] + Right * HalfSubnodeLength[12] + Bottom * HalfSubnodeLength[12]),
                (int)(Front * HalfSubnodeLength[12] + Left * HalfSubnodeLength[12] + Top * HalfSubnodeLength[12]),
                (int)(Front * HalfSubnodeLength[12] + Right * HalfSubnodeLength[12] + Top * HalfSubnodeLength[12])
            },
            new int[]
            {
                (int)(Back * HalfSubnodeLength[13] + Left * HalfSubnodeLength[13] + Bottom * HalfSubnodeLength[13]),
                (int)(Back * HalfSubnodeLength[13] + Right * HalfSubnodeLength[13] + Bottom * HalfSubnodeLength[13]),
                (int)(Back * HalfSubnodeLength[13] + Left * HalfSubnodeLength[13] + Top * HalfSubnodeLength[13]),
                (int)(Back * HalfSubnodeLength[13] + Right * HalfSubnodeLength[13] + Top * HalfSubnodeLength[13]),
                (int)(Front * HalfSubnodeLength[13] + Left * HalfSubnodeLength[13] + Bottom * HalfSubnodeLength[13]),
                (int)(Front * HalfSubnodeLength[13] + Right * HalfSubnodeLength[13] + Bottom * HalfSubnodeLength[13]),
                (int)(Front * HalfSubnodeLength[13] + Left * HalfSubnodeLength[13] + Top * HalfSubnodeLength[13]),
                (int)(Front * HalfSubnodeLength[13] + Right * HalfSubnodeLength[13] + Top * HalfSubnodeLength[13])
            }
        };
    }
}