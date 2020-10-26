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
    private int partNum = 2;
    private Material mat = default;
    private ComputeBuffer partBuf;
    private int kerIdx;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
        partBuf = new ComputeBuffer(partNum, Marshal.SizeOf(typeof(Particle)));
        Particle[] ps = new Particle[partNum];
        ps[0] = new Particle(new Vector3(0.0f, 0.0f, 0.0f));
        ps[1] = new Particle(new Vector3(0.5f, 0.0f, 0.0f));
        partBuf.SetData(ps);

        kerIdx = cs.FindKernel("CSMain");
        cs.SetBuffer(kerIdx, "Particles", partBuf);
    }

    // Update is called once per frame
    void Update()
    {
        cs.SetBuffer(kerIdx, "Particles", partBuf);
        cs.Dispatch(kerIdx, partNum, 1, 1);
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
