using Core.Octree;
using System;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Rendering
{
    public class AtherRenderer : MonoBehaviour
    {
        [SerializeField] private WorldDiscriptor worldParameters;
        private SparseOctree octree;

        [ContextMenu("Create")]
        void CreateOctree()
        {
            octree = new(worldParameters);
            visibleLayers = new bool2[worldParameters.maxDepth + 1];
        }

        [ContextMenu("Execute")]
        void Execute()
        {
            octree.Schedule(8, 1).Complete();
        }

        [ContextMenu("Dispose")]
        void Dispose()
        {
            octree.Dispose();
            Debug.Log("Disposed Renderer");
        }

        [Header("DEBUG SETTINGS")]

        [SerializeField] private bool debugNodes = true;
        [SerializeField] private float nodeScale = 1;
        [SerializeField] private float randomOffset = 0f;
        [SerializeField] private bool2[] visibleLayers;
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
                if (math.any(visibleLayers[node.Depth]) && node.IsLeaf)
                {
                    float3 random = 0;
                    if (randomOffset != 0)
                    {
                        random = Unity.Mathematics.Random.CreateFromIndex((uint)node.Position.GetHashCode()).NextFloat3(-1, 1) * randomOffset;
                    }
                    var scale = 1f / Mathf.Pow(2, node.Depth) * worldParameters.rootLength * nodeScale;
                    float sphereRadius = scale / 10f;

                    float3 position = node.Position + random;
                    float3 cubeSize = Vector3.one * scale;

                    if (visibleLayers[node.Depth][0]) Gizmos.DrawWireCube(position, cubeSize);
                    if (visibleLayers[node.Depth][1]) Gizmos.DrawSphere(position, sphereRadius);
                }

                if (!node.IsLeaf)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        DrawNode(node.Octants.nodes[i]);
                    }
                }
            }
        }

    }

}
