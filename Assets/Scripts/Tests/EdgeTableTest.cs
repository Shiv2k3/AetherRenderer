using Core.Rendering.Octree;
using Core.Util;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class OctantVisualizer : MonoBehaviour
{
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

    public static readonly int[][] Edges = new int[][]
    {
        new int[] {0, 1},
        new int[] {1, 3},
        new int[] {2, 3},
        new int[] {0, 2},
        new int[] {4, 5},
        new int[] {5, 7},
        new int[] {6, 7},
        new int[] {4, 6},
        new int[] {0, 4},
        new int[] {1, 5},
        new int[] {2, 6},
        new int[] {3, 7}
    };

    [ShowInInspector] private int[][] edgeTable = new int[256][];

    [Button]
    private void PrecalculateEdges()
    {
        for (int i = 0; i < 256; i++)
        {
            List<int> edges = new List<int>();

            for (int j = 0; j < UnitCorners.Length; j++)
            {
                if (((i >> j) & 1) == 1) // If the corner is active
                {
                    for (int k = 0; k < Edges.Length; k++) // add concerned edges
                    {
                        int[] edge = Edges[k];
                        if (edge[0] == j || edge[1] == j)
                        {
                            edges.Add(edge[0]);
                            edges.Add(edge[1]);
                        }
                    }
                }
            }

            edgeTable[i] = edges.ToArray();
        }
    }

    [Button]
    public void PrintEdgeTableAsConstant()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("public static readonly int[][] EdgeIndexTable = new int[256][]");
        sb.AppendLine("{");

        for (int i = 0; i < edgeTable.Length; i++)
        {
            var edges = edgeTable[i];
            var edgesList = new List<int>(edges.Length);
            for (int j = 0; j < edges.Length; j++)
            {
                edgesList.Add(edges[j]);
            }
            if (edges != null && edges.Length > 0)
            {
                sb.Append("    new int[] {");

                for (int j = 0; j < edges.Length; j++)
                {
                    sb.Append(edgesList[j]);

                    if (j < edges.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }

                sb.AppendLine($"}}, // {Convert.ToString(i, 2).PadLeft(8, '0')}");
            }
            else
            {
                sb.AppendLine("    new int[0],");
            }
        }

        sb.AppendLine("};");

        // Copy to clipboard in Unity Editor
        TextEditor te = new TextEditor { text = sb.ToString() };
        te.SelectAll();
        te.Copy();

        Debug.Log("Code copied to clipboard.");
    }


    [Range(0, 255)] public int bitmapIndex;
    void OnDrawGizmos()
    {
        int[] edges = edgeTable[bitmapIndex];
        if (edges == null) return;

        // Draw the edges
        Gizmos.color = Color.yellow;
        for (int i = 0; i < edges.Length; i += 2)
        {
            Vector3 start = new Vector3(UnitCorners[edges[i]].x, UnitCorners[edges[i]].y, UnitCorners[edges[i]].z);
            Vector3 end = new Vector3(UnitCorners[edges[i + 1]].x, UnitCorners[edges[i + 1]].y, UnitCorners[edges[i + 1]].z);

            Gizmos.DrawLine(start, end);
        }

        // Draw the corners
        for (int i = 0; i < UnitCorners.Length; i++)
        {
            bool isActive = ((bitmapIndex >> i) & 1) == 1;
            Gizmos.color = isActive ? Color.green : Color.red;
            Vector3 position = new Vector3(UnitCorners[i].x, UnitCorners[i].y, UnitCorners[i].z);
            Gizmos.DrawSphere(position, 0.05f);
            UnityEditor.Handles.Label(position + Camera.current.transform.right * 0.1f, i.ToString());
        }
    }
}
