Shader "Custom/DarkLighting" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_Transparency("Transparency", Range(0.0,0.5)) = 0.25
	}

	SubShader {
		Tags{ "Queue" = "Transparent" }
		Pass{
			// Removes overlapping transparency in sprites with alpha
			Stencil{
				Ref 2
				Comp NotEqual
				Pass Replace
			}

			Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				sampler2D _MainTex;
				float4 _Color;
				float _Transparency;

				struct appdata{
					float4 vertex : POSITION;
					float4 texcoord : TEXCOORD0;
				};

				struct v2f{
					float4 pos : SV_POSITION;
					float4 uv: TEXCOORD0;
				};

				v2f vert(appdata v){
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;

					return o;
				}

				float4 frag(v2f i) : SV_Target{
					float4 color = tex2D(_MainTex, i.uv);
				
					// if the transparency of a pixel is < 0 discard it
					if(color.a == 0.0)
						discard;
				
					color *= _Color;
					color.a = _Transparency;

					clip(color.a > _Transparency ? -1:1);

					return color;
				}
				ENDCG
		}
	}
}
