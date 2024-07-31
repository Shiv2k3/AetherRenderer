using UnityEngine;

public class NextBitTest : MonoBehaviour
{
    public bool[] bits = new bool[8];

    [ContextMenu("Run")]
    void Test()
    {
        for (int i = 0; i < 8; i++)
        {
            int mask = 1 << i;
            byte op = (byte)((int)Mathf.Pow(2, i) & mask);
            bits[i] = op == mask;
        }
    }
}