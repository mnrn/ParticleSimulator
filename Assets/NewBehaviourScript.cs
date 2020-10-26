using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Shader shader = default;
    [SerializeField] private ComputeShader cs = default;
    private Material mat = default;
    private ComputeBuffer posBuf;
    private float[] pos = new float[] { 0.0f, 0.0f, 0.0f };
    private int kerIdx;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
        posBuf = new ComputeBuffer(1, sizeof(float) * 3);
        posBuf.SetData(pos);
        kerIdx = cs.FindKernel("CSMain");
        cs.SetBuffer(kerIdx, "_Position", posBuf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderObject()
    {
        mat.SetBuffer("pos", posBuf);
        if (mat.SetPass(0))
        {
            Graphics.DrawProceduralNow(MeshTopology.Points, 1);
        }
    }

    private void OnDestroy()
    {
        posBuf.Release();
    }
}
