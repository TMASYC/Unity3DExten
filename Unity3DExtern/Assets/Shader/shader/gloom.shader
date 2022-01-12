// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "selfLight"
{
    Properties
    {
        _DiffuseColor ("DiffuseColor", Color) = (1, 1, 1, 1)
        _SpecColor1 ("SpecColor", Color) = (1, 1, 1, 1)
        _SpecPower ("SpecPower", Range(1, 90)) = 30
    }
    
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        pass
        {
            
            
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Lighting.cginc"
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            
            struct VertexInput
            {
                float4 vertex: POSITION;   // 顶点信息 Get✔
                float4 normal: NORMAL;     // 法线信息 Get✔
            };
            
            struct v2f2
            {
                float4 worldPos: TEXCOORD0;
                float4 vert: SV_POSITION;
                float3 worldNor: TEXCOORD1;
            };
            
            uniform float3 _SpecColor1;
            uniform float3 _DiffuseColor;
            uniform float _SpecPower;
            
            v2f2 vert(VertexInput i)
            {
                v2f2 v;
                
                float4 verte = UnityObjectToClipPos(i.vertex);
                v.vert = verte;
                
                float4 worldPos = mul(unity_ObjectToWorld, i.vertex);
                v.worldPos = worldPos;
                
                
                float3 worldNor = (UnityObjectToWorldNormal(i.normal));
                v.worldNor = worldNor;
                
                // float3 diffuse = _LightColor0.xyz * ndotl * _DiffuseColor;
                // float3 reflectLightDir = reflect(-lightDir, worldNor);
                // float rdotl = saturate(dot(reflectLightDir, viewDir));
                // float3 specular = _LightColor0.xyz * _SpecColor1 * pow(rdotl, _SpecPower);
                // v.Color = specular + diffuse;
                
                return v;
            }
            
            
            float4 frag(v2f2 i): COLOR
            {
                float3 nDir = normalize(i.worldNor);
                float3 lightDir = (_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                float3 halfDir = normalize(lightDir + viewDir);
                
                float ndoth = dot(nDir, halfDir);
                float ndotl = saturate(dot(nDir, lightDir));
                
                
                float spec = pow(max(0.0, ndoth), _SpecPower);
                
                
                
                //把视线方向归一化
                
                
                //顶点转化到世界空间
                //float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //计算视线方向，物体在世界坐标下的位置减去摄像机的位置，就是实现方法（这个方向是指向屏幕的，不是从屏幕射出去）
                //计算视线方向与法线方向的夹角，夹角越大，dot值越接近0，说明视线方向越偏离该点，也就是平视，该点越接近边缘
                float rim = 1 - max(0, dot(viewDir, nDir));  // o.worldViewDir = _WorldSpaceCameraPos.xyz - worldPos;
                //计算rimLight
                fixed3 rimColor = _SpecColor1 * pow(rim, 5);
                float3 finalColor = _DiffuseColor * ndotl + spec + rimColor;
                
                return float4(finalColor, 1);
            }
              
            ENDCG
            
        }
    }
    
    Fallback "Diffuse"
}