Shader "Custom/NewSurfaceShader"
{
    SubShader
    {
        ZWrite On

        Pass 
        {            
            CGPROGRAM

            // シェーダーモデルは5.0を指定しておきます。
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            StructuredBuffer<float3> pos;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float sz : PSIZE;
            };

            v2f vert(uint id: SV_VertexID)
            {
                v2f output;
                output.pos = float4(pos[id], 1.0);
                output.sz = 100.0;
                return output;
            }

            fixed4 frag(v2f input) : COLOR
            {
                return float4(1.0, 1.0, 1.0, 1.0);
            }

            ENDCG
        }
    }
}
