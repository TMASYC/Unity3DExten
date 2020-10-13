// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TMAS/Image"
{
    Properties
    {
        _MainTex("Main Tex",2D) = "white"{}
        _Tint("Color",Color) = (1,1,1,1)
        _BaseColor("Color",Color) = (0,0,0,1)
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }  
    SubShader
    {
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent"}
    

        Pass
        {
            ZWrite Off
            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            ColorMask[_ColorMask]
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Tint;
            fixed4 _BaseColor;

            struct a2v
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv :TEXCOORD0;
                float4 pos : SV_POSITION;
            };
            v2f vert(a2v a)
            {
                v2f f;
                f.uv = TRANSFORM_TEX(a.texcoord, _MainTex);
                f.pos = UnityObjectToClipPos(a.vertex);
                return f;
            }
            fixed4 frag(v2f i) :SV_Target
            {
                fixed4 col = tex2D(_MainTex,i.uv);
                fixed4 colo = col * _BaseColor + fixed4(_Tint.rgb,0);
                return colo;
            }
            ENDCG
        }
    }

}