using Core.Rendering.Octree;
using Core.Util;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Rendering
{
    public class AetherRenderer : MonoBehaviour
    {
        [SerializeField] private Settings world;
        private SparseOctree octree;
        private JobHandle octreeJob;
        [SerializeField, ReadOnly] private bool Disposed;

        [ContextMenu("Create")]
        void CreateOctree()
        {
            octree = new(world);
            world.octreeCenter = 0;
            draw = true;
            Disposed = false;
        }

        [ContextMenu("Execute")]
        void Execute()
        {
            System.Diagnostics.Stopwatch t = new();
            t.Start();

            SparseOctree.Settings.Data = world;
            octree.ResetPool();
            octreeJob = octree.Schedule(8, 1);
            octreeJob.Complete();

            t.Stop();
            Debug.Log("Completion Time " + t.Elapsed.TotalMilliseconds + "MS");
        }

        private float3 CameraPosition
        {
            get
            {
                try { return UnityEngine.Camera.current.transform.position; }
                catch
                {
                    return 0;
                }
            }
        }

        private void Awake() => CreateOctree();

        private void Update()
        {
            if (Disposed) return;

            float distance = math.distance(CameraPosition, world.octreeCenter);
            if (distance > world.updateThreshold)
            {
                world.octreeCenter = CameraPosition;
                Execute();
            }
#if DEBUG
            world.nodesAvailable = octree.NodesAvailable();
#endif
        }

        [ContextMenu("Dispose")]
        void Dispose()
        {
            octree.Dispose();
            Debug.Log("Disposed Renderer");
            Disposed = true;
        }

        [Header("DEBUG SETTINGS")]

        [SerializeField] private bool draw = true;
        [SerializeField, ShowIf("@draw")] private bool syncJob = false;
        [SerializeField, ShowIf("@draw")] private bool internalNodes = true;
        [SerializeField, ShowIf("@draw")] private bool exceptions = false;
        [SerializeField, ShowIf("@draw")] private bool lodValue = false;

        [SerializeField, ShowIf("@draw")] private float nodeScale = 1;
        [SerializeField, ShowIf("@draw")] private float randomOffset = 0f;


        [Header("Octree layer visualizer")]
        [SerializeField, ShowIf("@draw")] private List<DebugPram> visibleLayers = new(Settings.MaxDepth);
        [Button] void EnableCenter() => visibleLayers.ForEach((x) => {  x.DrawCenter = true; });
        [Button] void DisableCenter() => visibleLayers.ForEach((x) => {  x.DrawCenter = false; });

        [Serializable]
        class DebugPram
        {
            public bool DrawBounds;
            public bool DrawCenter;
            public Color color;
            public bool ShouldDraw => DrawBounds || DrawCenter;
        }

        private unsafe void OnDrawGizmos()
        {
            if (!draw ||  Disposed) return;
            if (syncJob && !octreeJob.IsCompleted) return;
            
            DrawNode(octree.root, 0, 0);

            void DrawNode(in Node node, in int depth, in float3 position)
            {
                if (visibleLayers[depth].ShouldDraw && (node.IsLeaf || internalNodes || node.isChunkPtr))
                {
                    float3 random = 0;
                    if (randomOffset != 0)
                    {
                        random = Unity.Mathematics.Random.CreateFromIndex((uint)node.GetHashCode()).NextFloat3(-1, 1) * randomOffset;
                    }
                    var scale = SparseOctree.SubnodeLength(depth) * nodeScale;
                    float sphereRadius = scale / 10f;

                    float3 pos = position + random;
                    float3 cubeSize = Vector3.one * scale;

                    Gizmos.color = visibleLayers[depth].color;
                    if (node.isChunkPtr)
                    {
                        Gizmos.DrawCube(pos, cubeSize);
                        return;
                    }
                    if (visibleLayers[depth].DrawBounds) Gizmos.DrawWireCube(pos, cubeSize);
                    if (visibleLayers[depth].DrawCenter) Gizmos.DrawSphere(pos, sphereRadius);
                    if (lodValue)
                    {
                        UnityEditor.Handles.Label(pos, $"{octree.SubnodeLOD(pos)}");
                    }
                }

                if (!node.IsLeaf)
                {
                    int _depth = depth + 1;
                    for (int child = 0, start = 0; child < node.Count; child++)
                    {
                        SparseOctree.FindNext1(node._map, ref start, out int octantIndex);
                        start = octantIndex + 1;
                        float3 pos = SparseOctree.SubnodePosition(octantIndex, _depth, position);
                        try
                        {
                            DrawNode(node[child], _depth, pos); 
                        }
                        catch
                        {
                            if (!exceptions) continue;

                            float3 s = SparseOctree.SubnodeLength(_depth);
                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(pos, s);
                        }

                    }
                }
            }
        }

    }

}