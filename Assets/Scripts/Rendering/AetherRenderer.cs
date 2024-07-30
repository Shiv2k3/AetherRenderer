using Core.Octree;
using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Core.Rendering
{
    public class AetherRenderer : MonoBehaviour
    {
        [SerializeField] private WorldDiscriptor worldParameters;
        private SparseOctree octree;

        [ContextMenu("Create")]
        void CreateOctree()
        {
            JobsUtility.JobWorkerCount = Environment.ProcessorCount - 1;
            octree = new(worldParameters);
            lastPos = 0;
            debugNodes = true;
        }

        [ContextMenu("Execute")]
        void Execute()
        {
            System.Diagnostics.Stopwatch t = new();
            t.Start();

            octree.Camera = Camera;
            octree.Schedule(8, 1).Complete();

            t.Stop();
            Debug.Log("Completion Time " + t.Elapsed.TotalMilliseconds + "MS");
        }

        [SerializeField] private float3 lastPos;
        [SerializeField] private float updateCheckRadius;

        private float3 Camera => UnityEngine.Camera.current.transform.position;

        private void Awake() => CreateOctree();

        private void Update()
        {
            float distance = math.distance(Camera, lastPos);
            if (distance > updateCheckRadius)
            {
                lastPos = Camera;
                Execute();
            }
        }

        [ContextMenu("Dispose")]
        void Dispose()
        {
            octree.Dispose();
            Debug.Log("Disposed Renderer");
        }

        [Header("DEBUG SETTINGS")]

        [SerializeField] private bool debugNodes = true;
        [SerializeField] private bool debugParents = true;
        [SerializeField] private float nodeScale = 1;
        [SerializeField] private float randomOffset = 0f;
        [SerializeField] private List<DebugPram> visibleLayers = new(14);

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
            try
            {
                if (debugNodes)
                {
                    DrawNode(octree.root);
                }
            }
            catch (Exception e)
            {

                debugNodes = false;
                Debug.LogError("Gizmo Error: " + e);
            }

            void DrawNode(in Node node)
            {
                if (visibleLayers[node.Depth].ShouldDraw && (node.IsLeaf || debugParents))
                {
                    float3 random = 0;
                    if (randomOffset != 0)
                    {
                        random = Unity.Mathematics.Random.CreateFromIndex((uint)node.GetHashCode()).NextFloat3(-1, 1) * randomOffset;
                    }
                    var scale = SparseOctree.OctantLength(node.Depth) * nodeScale;
                    float sphereRadius = scale / 10f;

                    float3 position = node.Position + random;
                    float3 cubeSize = Vector3.one * scale;

                    Gizmos.color = visibleLayers[node.Depth].color;
                    if (visibleLayers[node.Depth].DrawBounds) Gizmos.DrawWireCube(position, cubeSize);
                    if (visibleLayers[node.Depth].DrawCenter) Gizmos.DrawSphere(position, sphereRadius);

                    float distToCam = math.distance(Camera, node.Position) / (worldParameters.rootLength / 2f) * worldParameters.maxDepth;
                    //float nodeLOD = math.log2(SparseOctree.OctantLength(node.Depth) / SparseOctree.OctantLength(worldParameters.maxDepth)) * distToCam;
                    float nodeLOD = math.clamp(distToCam, 0, worldParameters.maxDepth);
                    Handles.Label(node.Position, $"{nodeLOD}");
                }

                if (!node.IsLeaf)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        DrawNode(node.Children[i]);
                    }
                }
            }
        }

    }

}