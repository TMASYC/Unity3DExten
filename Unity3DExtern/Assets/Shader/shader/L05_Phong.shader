Shader "Unlit/L05_Phong"
{
    Properties
    {
        _Spec ("Specualr", Range(1, 10)) = 1
        _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
        _SpecularColor ("SpecColor", Color) = (1, 1, 1, 1)
        
        _SpecularGlass ("SpecularGlassStrength", Range(0, 64)) = 32
        _ObjColor ("ObjColor", color) = (1, 1, 1, 1)
        _RimColor ("RimColor", color) = (1, 1, 1, 1)
        _RimStrength ("RimStrength", Range(0.0001, 3.0)) = 0.1
        
        _AtmoColor ("Atmosphere Color", Color) = (0, 0.4, 1.0, 1)
        _Size ("Size", Float) = 0.1
        _OutLightPow ("LightPow", Float) = 5
        _OutLightStrength ("Strength", Float) = 15
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
                float3 normal: NORMAL;
            };
            
            struct v2f
            {
                float4 vertex: SV_POSITION;
                float3 ndir: TEXCOORD0;
                float3 worldPos: TEXCOORD1;
            };
            
            //uniform 共享于vert,frag
            //attibute 仅用于vert
            //varying 用于vert frag传數據
            
            uniform float _Spec;
            uniform float3 _Diffuse;
            uniform float3 _SpecularColor;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.ndir = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            fixed4 frag(v2f i): SV_Target
            {
                fixed3 nor = normalize(i.ndir);
                fixed3 lightdir = normalize(_WorldSpaceLightPos0.xyz);
                
                //fixed lambert = max(0.0, dot(nor, lightdir));
                fixed ndotl = dot(nor, lightdir);
                fixed halfLambert = ndotl * 0.5 + 0.5;
                
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                
                fixed3 diffuse = _LightColor0.xyz * _Diffuse.xyz * halfLambert;
                
                fixed3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos.xyz);// - //normalize(UnityWorldSpaceViewDir(i.vertex.xyz));
                
                fixed3 reflectDir = normalize(reflect(-lightdir, nor));
                
                fixed3 spec = _LightColor0.rgb * _SpecularColor.rgb * pow(saturate(dot(viewDir, reflectDir)), _Spec);
                
                
                return fixed4(spec, 0);
            }
            ENDCG
            
        }
        
        Pass  //pass2 实现OutLight
        {
            Name "AtmosphereBase"
            Tags { "LightMode" = "Always" }
            
            Cull Front
            Blend SrcAlpha One
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            uniform float _Size;
            uniform float4 _AtmoColor;
            uniform float _OutLightPow;
            uniform float _OutLightStrength;
            
            struct v2f
            {
                float3 normal: TEXCOORD0;
                float3 worldPos: TEXCOORD1;
                float4 vertex: SV_POSITION;
            };
            
            v2f vert(appdata_base v)
            {
                v2f o;
                v.vertex.xyz += v.normal * _Size;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            fixed4 frag(v2f i): COLOR
            {
                i.normal = normalize(i.normal);
                //视角观察方向
                float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos.xyz);
                
                float4 color = _AtmoColor;
                
                color.a = pow(saturate(dot(viewDir, i.normal)), _OutLightPow);
                color.a *= _OutLightStrength * dot(viewDir, i.normal);
                return color;
            }
            ENDCG
            
        }
    }
}
