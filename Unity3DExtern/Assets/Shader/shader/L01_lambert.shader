Shader "Unlit/L01_lambert"
{
    Properties
    {
        _Diffuse ("diffuse", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            struct appdata
            {
                float4 vertex: POSITION;
                float3 noraml: NORMAL;
            };
            
            struct v2f
            {
                float4 vertex: SV_POSITION;
                float3 noraml: TEXCOORD0;
            };
            
            fixed4 _Diffuse;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.noraml = UnityObjectToWorldNormal(v.noraml);
                //fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex)
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }
            
            fixed4 frag(v2f i): SV_Target
            {
                fixed3 nor = normalize(i.noraml);
                fixed3 lightdir = normalize(_WorldSpaceLightPos0.xyz);
                
                //fixed lambert = max(0.0, dot(nor, lightdir));
                fixed ndotl = dot(nor, lightdir);
                fixed halfLambert = ndotl * 0.5 + 0.5;
                
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                
                fixed3 diffuse = _LightColor0.xyz * _Diffuse.xyz * halfLambert;
                
                // sample the texture
                
                // apply fog
                
                
                return fixed4(diffuse + ambient, 1);
            }
            ENDCG
            
        }
    }
}
