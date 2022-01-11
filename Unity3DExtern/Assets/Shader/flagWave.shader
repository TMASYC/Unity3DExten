Shader "Unlit/flagWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _Heright ("Height", float) = 1
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
            // make fog work
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv: TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex: SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            v2f vert(appdata v)
            {
                v2f o;
                
                
                //UNITY_TRANSFER_FOG(o, o.vertex);
                float time = _Time.y * 2;
                //float wave = 1 * sin(time)
                v.vertex.z = v.vertex.z + 1 * sin(time + v.vertex.x) * v.uv.x;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
                
                // v2f o;
                // float time = _Time.y * 2;//时间周期
                // float wavar = 1 * sin(time + v.vertex.x * 0.5);
                // v.vertex.y = v.vertex.y + wavar;
                // o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = v.uv;
                // return o;
            }
            
            fixed4 frag(v2f i): SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
            
        }
    }
}
