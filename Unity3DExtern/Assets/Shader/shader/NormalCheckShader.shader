// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/NormalCheckShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog
            
            //#include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            
            struct appdata
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
                float4 normal: NORMAL;
            };
            
            struct v2f
            {
                float2 uv: TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex: SV_POSITION;
                float3 normal: TEXCOORD1;
                float3 color: TEXCOORD2;
                float3 ndotl: TEXCOORD3;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = normalize(UnityObjectToWorldNormal(v.normal)) ;
                //UNITY_TRANSFER_FOG(o, o.vertex);
                fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 light = _LightColor0.xyz;
                o.ndotl = saturate(dot(o.normal, lightDir) * 0.5 + 0.5);
                
                o.color = (_LightColor0.xyz * o.ndotl);
                return o;
            }
            //lambert
            
            fixed4 frag(v2f i): SV_Target
            {
                // sample the texture
                
                //  half4 BRDF1_Unity_PBS(half3 diffColor, half3 specColor, half oneMinusReflectivity, half smoothness,
                //   float3 normal, float3 viewDir,
                
                //UNITY_BRDF_PBS(i.albedo, );
                
                
                return fixed4(i.color, 1);
            }
            ENDCG
            
        }
    }
}
