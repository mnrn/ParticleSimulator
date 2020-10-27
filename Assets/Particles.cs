using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// パーティクル構造体 (こちらを頂点シェーダーとコンピュートシェーダーに渡します。)
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

public class Particles : MonoBehaviour
{
    // 描画用シェーダー
    [SerializeField] private Shader shader = default;
    // コンピュートシェーダー
    [SerializeField] private ComputeShader cs = default;

    // x軸方向のパーティクルの数 (コードではランダムに任せて実際の数値とは違います。ランダムに任せない場合使用してください。)
    [SerializeField] private int partNumX = default;
    // y軸方向のパーティクルの数 (コードではランダムに任せて実際の数値とは違います。ランダムに任せない場合使用してください。)
    [SerializeField] private int partNumY = default;
    // z軸方向のパーティクルの数 (コードではランダムに任せて実際の数値とは違います。ランダムに任せない場合使用してください。)
    [SerializeField] private int partNumZ = default;
    // GPUのx軸方向の分割数
    private const int LOCAL_SZ_X = 1024;

    // マテリアル
    private Material mat = default;
    // GPUに転送するバッファ
    private ComputeBuffer partBuf = default;
    // 今回使用するコンピュートシェーダのカーネル (ひとつだけです。)
    private int kerIdx;
    // パーティクルの総数
    private int partNum;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);

        // パーティクルの初期位置を決定します。
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
        // コンピュートシェーダーの実行
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
