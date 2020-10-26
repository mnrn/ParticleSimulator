Shader "Custom/NewSurfaceShader"
{
    SubShader
    {
        ZWrite Off
        Blend One One

        Pass 
        {            
            CGPROGRAM

            // シェーダーモデルは5.0を指定しておきます。
            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct Particle
            {
                float3 pos;
            };

            StructuredBuffer<Particle> particles;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float sz : PSIZE;
            };

            v2f vert(uint id: SV_VertexID)
            {
                v2f output;
                output.pos = float4(particles[id].pos, 1.0);
                output.pos = mul (UNITY_MATRIX_VP, output.pos);
                output.sz = 5.0;
                return output;
            }

            fixed4 frag(v2f input) : COLOR
            {
                return float4(0.015, 0.05, 0.3, 1.0);
            }

            ENDCG
        }
    }
}
