using Core.Rendering.Octree;
using System.Collections.Generic;
using UnityEngine;

public class NextBitTest : MonoBehaviour
{
    public bool[] bits = new bool[8];
    public List<int> indices = new();

    [ContextMenu("Run")]
    void Test()
    {
        indices.Clear();
        byte map = 0;
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            if (bits[i])
            {
                map |= (byte)(1 << i);
                count++;
            }
        }
        for (int i = 0, index = 0; i < count; i++)
        {
            SparseOctree.FindNext1(map, ref index, out var nextBitIndex);
            indices.Add(nextBitIndex);
            index = nextBitIndex + 1;
        }
    }

}