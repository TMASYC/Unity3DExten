Shader "Custom/scan_scene"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Progress("Progress",Range(-2, 2)) = 0.2
		_ScanColor("ScanColor", Color) = (1, 1, 1, 1)
		_Width("LightWidth",Range(0.01, 0.5)) = 0.05
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
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float3 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Progress;
				float4 _ScanColor;
				float _Width;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
					o.uv.z = v.vertex.x;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float halfWidth = _Width;
					fixed4 col = tex2D(_MainTex, i.uv);
					if (i.uv.z > _Progress - halfWidth && i.uv.z < _Progress + halfWidth)
					{
						float dis = (halfWidth - abs(i.uv.z - _Progress)) / halfWidth;
						col.rgb = col.rgb + _ScanColor * dis;
					}
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG
			}
		}
}