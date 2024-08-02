using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Core.Util
{
    public struct IndexPosition
    {
        // Cell index position methods, position [-size / 2, size / 2], index [0, size^3]
        public static float3 CellPosition(in int cellIndex, in int resolution)
        {
            float3 cellPosition = PositionFromIndex(cellIndex, resolution);
            float edge = resolution / 2f - 0.5f;
            return cellPosition -= edge;
        }
        public static int CellIndex(float3 cellPosition, in int resolution)
        {
            float edge = resolution / 2f - 0.5f;
            cellPosition += edge;
            return IndexFromPosition(cellPosition, resolution);
        }
        public static float3 CellPosition(in int cellIndex, in float3 resolution)
        {
            float3 cellPosition = PositionFromIndex(cellIndex, resolution);
            float3 edge = resolution / 2f - 0.5f;
            return cellPosition -= edge;
        }
        public static float2 CellPosition(int cellIndex, float2 resolution)
        {
            float2 cellPosition = PositionFromIndex2D(cellIndex, resolution);
            float2 edge = resolution / 2f - 0.5f;
            return cellPosition -= edge;
        }

        public static int CellIndex(float3 cellPosition, in float3 resolution)
        {
            float3 edge = resolution / 2f - 0.5f;
            cellPosition += edge;
            return IndexFromPosition(cellPosition, resolution);
        }
        public static float3 GetCell(float3 position, in float3 bound, in float density, out float3 cellSize)
        {
            position /= 2f;
            float3 edge = bound / 2f - 0.5f;
            position = clamp(position, -edge, edge);

            float3 resolution = bound * density;
            cellSize = bound / resolution;
            float3 x = float3(resolution % 2 == 0) * (cellSize / 2f);
            return (round((position - x) / cellSize) * cellSize + x) * 2;
        }
        public static float2 GetCell(float2 position, in float2 bound, in float density, out float2 cellSize)
        {
            position /= 2f;
            float2 edge = bound / 2f - 0.5f;
            position = clamp(position, -edge, edge);

            float2 resolution = bound * density;
            cellSize = bound / resolution;
            float2 x = float2(resolution % 2 == 0) * (cellSize / 2f);
            return (round((position - x) / cellSize) * cellSize + x) * 2;
        }

        // Corner index position methods
        public static int CornerIndex(float3 cornerPosition, in int resolution)
        {
            float edge = resolution / 2f;
            cornerPosition += edge;
            return IndexFromPosition(cornerPosition, resolution + 1);
        }
        public static float3 CornerPosition(in int cornerIndex, in int resolution)
        {
            float3 cornerPos = PositionFromIndex(cornerIndex, resolution + 1);
            float edge = resolution / 2f;
            return cornerPos -= edge;
        }
        public static float3 CornerPosition(in int cornerIndex, in int resolution, out bool border)
        {
            float3 cornerPos = PositionFromIndex(cornerIndex, resolution + 1);
            border = any(cornerPos >= resolution) || any(cornerPos <= 0);

            float edge = resolution / 2f;
            return cornerPos -= edge;
        }
        public static int CornerIndex(float3 cornerPosition, in float3 resolution)
        {
            float3 edge = resolution / 2f;
            cornerPosition += edge;
            return IndexFromPosition(cornerPosition, resolution + 1);
        }
        public static float3 CornerPosition(in int cornerIndex, in float3 resolution)
        {
            float3 cornerPos = PositionFromIndex(cornerIndex, resolution + 1);
            float3 edge = resolution / 2f;
            return cornerPos -= edge;
        }


        // Standard index to position methods, position [0, size], index [0, size^3]
        public static float3 PositionFromIndex(int index, in int size)
        {
            int z = index / (size * size);
            index -= z * size * size;
            int y = index / size;
            int x = index % size;

            return new float3(x, y, z);
        }
        public static int IndexFromPosition(float3 position, int size)
        {
            return (int)((position.z * size * size) + (position.y * size) + position.x);
        }
        public static float3 PositionFromIndex(int index, in float3 size)
        {
            int z = (int)(index / (size.x * size.y));
            index -= (int)(z * size.x * size.y);
            int y = (int)(index / size.x);
            int x = (int)(index % size.x);
            return float3(x, y, z);
        }
        public static float2 PositionFromIndex2D(in int index, in float2 size)
        {
            int y = (int)(index / size.x);
            int x = (int)(index % size.x);
            return float2(x, y);
        }

        public static int IndexFromPosition(in float3 position, in float3 size)
        {
            return (int)((position.z * size.x * size.y) + (position.y * size.x) + position.x);
        }
    }
}