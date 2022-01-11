Shader "selfLight"
{
    Properties
    {
        _DiffuseColor ("DiffuseColor", Color) = (1, 1, 1, 1)
        _SpecColor1 ("SpecColor", Color) = (1, 1, 1, 1)
        _SpecPower ("SpecPower", Float) = 3
    }
    
    SubShader
    {
        
        pass
        {
            
            Tags { "RenderType" = "Opaque" }
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Lighting.cginc"
            #include "UnityCG.cginc"
            
            
            
            struct v2f2
            {
                float2 uv: TEXCOORD0;
                float4 vertex: POSITION;
                float3 normal: NORMAL0;
                float3 finalColor: TEXCOORD1;
            };
            
            uniform float3 _SpecColor1;
            uniform float3 _DiffuseColor;
            uniform float _SpecPower;
            
            v2f2 vert(appdata_base i)
            {
                v2f2 v;
                v.vertex = UnityObjectToClipPos(i.vertex);
                v.normal = UnityObjectToWorldNormal(i.normal);
                
                fixed3 lightDir = _WorldSpaceLightPos0.xyz;
                
                float halflambert = saturate(dot(v.normal, lightDir) * 0.5 + 0.5);
                
                float3 diffuse = _DiffuseColor * halflambert * _LightColor0.xyz;
                
                float3 halfAngleDir = normalize(WorldSpaceViewDir(v.vertex) + v.normal);
                
                float3 hadotn = saturate(dot(halfAngleDir, v.normal));
                
                float3 spec = _LightColor0.xyz * _SpecColor1.xyz * pow(hadotn, _SpecPower);
                
                v.finalColor = spec + diffuse;
                
                return v;
            }
            
            
            float4 frag(v2f2 i): COLOR
            {
                return float4(i.finalColor, 0);
            }
            
            ENDCG
            
        }
    }
    
    Fallback "Diffuse"
}