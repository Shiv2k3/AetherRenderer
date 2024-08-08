using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Util
{
    [System.Serializable]
    public struct Settings
    {
        public const int WorldSize = 8_192;
        public const int HalfWorldSize = 4_096;
        public const int MaxDepth = 9;

        [Tooltip("The pool uses this to calculate the number of nodes to allocate")]
        [Min(1)] public int poolDepth;

        [Tooltip("The radius of the test sphere")]
        [Min(0)] public float sphereRadius;

        [Tooltip("The LOD factor")]
        [Min(0)] public float lodFactor;

        [Tooltip("The amount of offset added to each octree during sampling")]
        [Min(0)] public float samplingOffset;

        [Tooltip("The amount of spread to put on the sample offset, lower value makes offset align with octant normal to reduce unifromity on other axies")]
        [Min(0)] public float sampleSpread;

        [Tooltip("The distance required between last and new octree center to trigger an update")]
        [Min(0)] public float updateThreshold;

        [Tooltip("The total number of nodes in the pool")]
        [ReadOnly] public int nodesUsed;

        [Tooltip("The position used to calculate the SVO")]
        [ReadOnly] public float3 octreeCenter;
    }
}