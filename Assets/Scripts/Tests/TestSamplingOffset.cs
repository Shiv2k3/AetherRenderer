using Core.Util;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class TestSamplingOffset : MonoBehaviour
{
    [SerializeField] float3 position;
    [SerializeField] float sampleSpread;
    [SerializeField] float samplingOffset;
    [SerializeField] int depth;

    [SerializeField] private float radius = 0.1f;
    private void OnDrawGizmos()
    {
        Unity.Mathematics.Random rng = new(1);
        //Gizmos.DrawWireCube(Vector3.zero, Vector3.one * Table.SubnodeLength[depth]);

        float3 range = Table.HalfSubnodeLength[depth] * samplingOffset * (math.normalize(position) / sampleSpread);
        float3 p = position + rng.NextFloat3(-range, range);

        Gizmos.DrawSphere(p, radius);
        Gizmos.DrawWireCube(Vector3.zero, range);
    }
}