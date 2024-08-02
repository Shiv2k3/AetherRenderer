using System;
using UnityEngine;

namespace Core.Rendering.Octree
{
    [Serializable]
    public struct WorldDiscriptor
    {
        [Min(0)] public int childrenPerThread;
        [Min(0)] public int maxDepth;
        [Min(1)] public float rootLength;
        [Min(1)] public float sphereRadius;
    }
}