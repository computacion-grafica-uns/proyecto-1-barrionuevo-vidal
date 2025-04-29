Shader "SurfaceShader"
{
    SubShader
    {
        Pass
        {
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            uniform float4x4 _ModelMatrix;
            uniform float4x4 _ProjectionMatrix;
            uniform float4x4 _ViewMatrix;

            struct appdata {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.color = v.color;
                o.vertex = mul(mul(_ProjectionMatrix, mul(_ViewMatrix,_ModelMatrix)), v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return (i.color);
            }
            ENDCG
        }
    }
}
