using Core.Rendering.Octree;
using Core.Util;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Core.Rendering
{
    public class AetherRenderer : MonoBehaviour
    {
        [SerializeField] private Settings world;
        private SparseOctree octree;

        [SerializeField, ReadOnly] private bool Disposed;

        [ContextMenu("Create")]
        void CreateOctree()
        {
            octree = new(world);
            world.octreeCenter = 0;
            Disposed = false;
        }

        [ContextMenu("Execute")]
        void Execute()
        {
            System.Diagnostics.Stopwatch t = new();
            t.Start();

            SparseOctree.settings.Data = world;
            octree.Reset();
            octree.Schedule(8, 1).Complete();

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
            world.nodesUsed = octree.NodesUsed();
#endif
        }

        [ContextMenu("Dispose")]
        void Dispose()
        {
            octree.Dispose();
            Debug.Log("Disposed Renderer");
            Disposed = true;
        }

#if DEBUG
        [Header("DEBUG SETTINGS")]

        [SerializeField] private bool draw = true;
        [SerializeField, ShowIf("@draw")] private bool nodes = false;
        [SerializeField, ShowIf("@draw")] private bool internalNodes = true;
        [SerializeField, ShowIf("@draw")] private bool exceptions = false;
        [SerializeField, ShowIf("@draw")] private bool lodValue = false;
        [SerializeField, ShowIf("@draw")] private bool voxel = false;
        [SerializeField, ShowIf("@draw")] private bool vertices = false;
        [SerializeField, ShowIf("@draw")] private bool faces = false;

        [SerializeField, ShowIf("@draw")] private float nodeScale = 1;
        [SerializeField, ShowIf("@draw")] private float randomOffset = 0f;
        [SerializeField, ShowIf("@draw")] private float pointScale = 1f;
        [SerializeField, ShowIf("@draw")] private float lineScale = 1f;
        
        [Header("Octree layer visualizer")]
        [SerializeField, ShowIf("@draw")] private List<DebugPram> visibleLayers = new(Settings.MaxDepth);
        [Button] void EnableBound() => visibleLayers.ForEach((x) => {  x.DrawBounds = true; });
        [Button] void DisableBound() => visibleLayers.ForEach((x) => {  x.DrawBounds = false; });

        private unsafe void OnDrawGizmos()
        {
            if (!draw ||  Disposed) return;
            if (nodes) DrawNode(octree.root, 0, 0);
            if (vertices)
            {
                var rng = Unity.Mathematics.Random.CreateFromIndex(0);
                foreach (var list in octree.featurePoints)
                {
                    foreach(var p in list)
                    {
                        float3 r = 0;
                        if (randomOffset != 0) r = rng.NextFloat3() * randomOffset;
                        var pos = p + r;
                        Gizmos.color = Color.blue;
                        Gizmos.DrawSphere(pos, pointScale);
                    }
                }
            }

            void DrawNode(in Node node, in int depth, in float3 position)
            {
                if (visibleLayers[depth].ShouldDraw && (node.IsLeaf || internalNodes) && (!voxel || (voxel && math.distance(CameraPosition, position) < SparseOctree.SubnodeLength(depth) * 2)))
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

                    if (visibleLayers[depth].DrawBounds) Gizmos.DrawWireCube(pos, cubeSize);
                    if (visibleLayers[depth].DrawCenter)
                    {
                        if (voxel)
                        {
                            //Gizmos.color = node._dataIndex.Density < 0 ? Color.red : Color.green;
                            //Gizmos.DrawLine(pos, pos + node._dataIndex.Normal * math.abs(node._dataIndex.Density) * lineScale);
                            //Handles.Label(pos + (float3)Camera.current.transform.right * 0.25f, node._dataIndex.Density.ToString());
                        }
                        Gizmos.DrawSphere(pos, pointScale);
                    }

                    if (lodValue)
                    {
                        UnityEditor.Handles.Label(pos, $"{octree.SubnodeLOD(pos)}");
                    }
                }

                if (!node.IsLeaf)
                {
                    int _depth = depth + 1;
                    for (int child = 0, start = 0; child < node.SubnodeCount; child++)
                    {
                        SparseOctree.FindNext1Bit(node._map, ref start, out int octantIndex);
                        start = octantIndex + 1;
                        float3 pos = SparseOctree.SubnodePosition(octantIndex, _depth, position);
                        try
                        {
                            DrawNode(node.Subnode(child), _depth, pos); 
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

        [System.Serializable]
        class DebugPram
        {
            public bool DrawBounds;
            public bool DrawCenter;
            public Color color;
            public bool ShouldDraw => DrawBounds || DrawCenter;
        }

#endif

    }
}