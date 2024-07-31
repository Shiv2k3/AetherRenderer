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
        [SerializeField] private int totalNodesPooled;

        private float3 Camera
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
            totalNodesPooled = 0;
            foreach (var threadSeg in SparseOctree.ChildrenPool.Data._thread_size_children)
            {
                int size = 1;
                foreach (var sizeSeg in threadSeg)
                {
                    totalNodesPooled += sizeSeg.Length * size;
                    size++;
                }
            }

            float distance = math.distance(Camera, lastPos);
            if (distance > updateCheckRadius)
            {
                lastPos = Camera;
                Execute();
            }
        }

        [ContextMenu("Release Root")]
        void ReleaseRoot()
        {
            octree.root.ReleaseNode();
            Debug.Log("Root Released");
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
        [SerializeField] private bool debugLOD = true;
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
            if (debugNodes) DrawNode(octree.root);

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

                    if (debugLOD)
                    {
                        float distToCam = math.distance(Camera, node.Position) / (worldParameters.rootLength / 2f) * worldParameters.maxDepth;
                        float nodeLOD = math.clamp(distToCam, 0, worldParameters.maxDepth);
                        Handles.Label(node.Position, $"{nodeLOD}");
                    }
                }

                if (!node.IsLeaf)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        try { DrawNode(node.Children[i]); }
                        catch
                        {
                            float3 p = SparseOctree.OctantPosition(i, node.Depth + 1, node.Position);
                            float3 s = SparseOctree.OctantLength(node.Depth + 1);

                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(p, s);
                        }

                    }
                }
            }
        }

    }

}