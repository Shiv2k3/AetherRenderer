using Core.Rendering.Octree;
using UnityEngine;

public class TestSubnodePosition : MonoBehaviour
{
    public int index = 0;
    public int depth = 0;
    public Vector3 pos;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < 8; i++)
        {
            var p = (Vector3)SparseOctree.SubnodePosition(index, depth, pos) + pos;
            var scale = SparseOctree.SubnodeLength(depth);
            Gizmos.DrawWireCube(p, Vector3.one * scale);
        }
    }
}