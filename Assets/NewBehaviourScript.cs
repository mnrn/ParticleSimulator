using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

struct Particle
{
    public Vector3 pos;

    public Particle(Vector3 pos)
    {
        this.pos = pos;
    }
};

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Shader shader = default;
    [SerializeField] private ComputeShader cs = default;
    [SerializeField ]private int partNum = default;
    private Material mat = default;
    private ComputeBuffer partBuf;
    private int kerIdx;
    private const int LOCAL_SZ_X = 256;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
        partBuf = new ComputeBuffer(partNum, Marshal.SizeOf(typeof(Particle)));
        Particle[] ps = new Particle[partNum];
        for (int i = 0; i < partNum; i++)
        {
            ps[i] = new Particle(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
        }
        partBuf.SetData(ps);

        kerIdx = cs.FindKernel("CSMain");
        cs.SetBuffer(kerIdx, "Particles", partBuf);
    }

    // Update is called once per frame
    void Update()
    {
        cs.SetBuffer(kerIdx, "Particles", partBuf);
        cs.Dispatch(kerIdx, partNum / LOCAL_SZ_X, 1, 1);
    }

    private void OnRenderObject()
    {
        mat.SetBuffer("particles", partBuf);
        if (mat.SetPass(0))
        {
            Graphics.DrawProceduralNow(MeshTopology.Points, partNum);
        }
    }

    private void OnDestroy()
    {
        partBuf.Release();
    }
}
