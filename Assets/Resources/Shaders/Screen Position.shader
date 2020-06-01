Shader "Unlit/Screen Position"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
			#pragma enable_d3d11_debug_symbols
            #pragma vertex vert
            #pragma fragment frag
			#pragma target 3.0
  

            #include "UnityCG.cginc"


            struct v2f
            {
                float2 uv : TEXCOORD0;
            };

            v2f vert (float4 vertex : POSITION, float2 uv : TEXCOORD0, out float4 outpos : SV_Position)
            {
				v2f o;
				o.uv = uv;
				outpos = UnityObjectToClipPos(vertex);
				return o;
            }

			sampler2D _MainTex;

			fixed4 frag(v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_TARGET
			{
				screenPos.xy = floor(screenPos.xy * 0.25) * 0.5;
				float checker = -frac(screenPos.r + screenPos.g);
				clip(checker);
				fixed4 c = tex2D(_MainTex, i.uv);
				return c;
			}
			
            ENDCG
        }
    }
}
