using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

struct Particle
{
    public Vector3 pos;
    public Vector3 vel;

    public Particle(Vector3 pos, Vector3 vel)
    {
        this.pos = pos;
        this.vel = vel;
    }
};

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Shader shader = default;
    [SerializeField] private ComputeShader cs = default;

    [SerializeField] private int partNumX = default;
    [SerializeField] private int partNumY = default;
    [SerializeField] private int partNumZ = default;
    private const int LOCAL_SZ_X = 1024;

    private Material mat = default;
    private ComputeBuffer partBuf = default;
    private int kerIdx;
    private int partNum;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);

        partNum = partNumX * partNumY * partNumZ;
        partBuf = new ComputeBuffer(partNum, Marshal.SizeOf(typeof(Particle)));
        Particle[] ps = new Particle[partNum];
        for (int i = 0; i < partNum; i++)
        {
            ps[i] = new Particle(
                new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)),
                new Vector3(0.0f, 0.0f, 0.0f)
            );
        }
        partBuf.SetData(ps);

        kerIdx = cs.FindKernel("CSMain");
        cs.SetBuffer(kerIdx, "ps", partBuf);
    }

    // Update is called once per frame
    void Update()
    {
        cs.SetBuffer(kerIdx, "ps", partBuf);
        cs.Dispatch(kerIdx, partNum / LOCAL_SZ_X, 1, 1);
    }

    private void OnRenderObject()
    {
        mat.SetBuffer("ps", partBuf);
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
