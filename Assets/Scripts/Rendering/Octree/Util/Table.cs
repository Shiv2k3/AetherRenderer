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

        public static readonly int[][] EdgeIndexTable = new int[256][]
        {
            new int[0],
            new int[] {0, 1, 0, 2, 0, 4}, // 00000001
            new int[] {0, 1, 1, 3, 1, 5}, // 00000010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5}, // 00000011
            new int[] {2, 3, 0, 2, 2, 6}, // 00000100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6}, // 00000101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6}, // 00000110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6}, // 00000111
            new int[] {1, 3, 2, 3, 3, 7}, // 00001000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7}, // 00001001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7}, // 00001010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7}, // 00001011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7}, // 00001100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7}, // 00001101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7}, // 00001110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7}, // 00001111
            new int[] {4, 5, 4, 6, 0, 4}, // 00010000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4}, // 00010001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4}, // 00010010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4}, // 00010011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4}, // 00010100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4}, // 00010101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4}, // 00010110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4}, // 00010111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4}, // 00011111
            new int[] {4, 5, 5, 7, 1, 5}, // 00100000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 5, 7, 1, 5}, // 00100001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5}, // 00100010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5}, // 00100011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5}, // 00100100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5}, // 00100101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5}, // 00100110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5}, // 00100111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5}, // 00101111
            new int[] {4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00110111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5}, // 00111111
            new int[] {6, 7, 4, 6, 2, 6}, // 01000000
            new int[] {0, 1, 0, 2, 0, 4, 6, 7, 4, 6, 2, 6}, // 01000001
            new int[] {0, 1, 1, 3, 1, 5, 6, 7, 4, 6, 2, 6}, // 01000010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 6, 7, 4, 6, 2, 6}, // 01000011
            new int[] {2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6}, // 01000100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6}, // 01000101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6}, // 01000110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6}, // 01000111
            new int[] {1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6}, // 01001111
            new int[] {4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01010111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6}, // 01011111
            new int[] {4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01100111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01101111
            new int[] {4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01110111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6}, // 01111111
            new int[] {5, 7, 6, 7, 3, 7}, // 10000000
            new int[] {0, 1, 0, 2, 0, 4, 5, 7, 6, 7, 3, 7}, // 10000001
            new int[] {0, 1, 1, 3, 1, 5, 5, 7, 6, 7, 3, 7}, // 10000010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 5, 7, 6, 7, 3, 7}, // 10000011
            new int[] {2, 3, 0, 2, 2, 6, 5, 7, 6, 7, 3, 7}, // 10000100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 5, 7, 6, 7, 3, 7}, // 10000101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 5, 7, 6, 7, 3, 7}, // 10000110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 5, 7, 6, 7, 3, 7}, // 10000111
            new int[] {1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 5, 7, 6, 7, 3, 7}, // 10001111
            new int[] {4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10010111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 5, 7, 6, 7, 3, 7}, // 10011111
            new int[] {4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10100111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10101111
            new int[] {4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10110111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 5, 7, 6, 7, 3, 7}, // 10111111
            new int[] {6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000000
            new int[] {0, 1, 0, 2, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000001
            new int[] {0, 1, 1, 3, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000011
            new int[] {2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11000111
            new int[] {1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11001111
            new int[] {4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11010111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11011111
            new int[] {4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11100111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11101111
            new int[] {4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110000
            new int[] {0, 1, 0, 2, 0, 4, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110001
            new int[] {0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110011
            new int[] {2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110110
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11110111
            new int[] {1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111000
            new int[] {0, 1, 0, 2, 0, 4, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111001
            new int[] {0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111010
            new int[] {0, 1, 0, 2, 0, 4, 0, 1, 1, 3, 1, 5, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111011
            new int[] {2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111100
            new int[] {0, 1, 0, 2, 0, 4, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7}, // 11111101
            new int[] {0, 1, 1, 3, 1, 5, 2, 3, 0, 2, 2, 6, 1, 3, 2, 3, 3, 7, 4, 5, 4, 6, 0, 4, 4, 5, 5, 7, 1, 5, 6, 7, 4, 6, 2, 6, 5, 7, 6, 7, 3, 7} // 11111110
        };

    }
}
