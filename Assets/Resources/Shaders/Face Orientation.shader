Shader "Unlit/Face Orientation"
{
    Properties 
	{
		_ColorFront ("Front Color", Color) = (1,1,1,1)
		_ColorBlack ("Back Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			float4 vert(float4 vertex : POSITION) :SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			fixed4 _ColorFront;
			fixed4 _ColorBlack;

			fixed4 frag(fixed facing : VFACE) : SV_Target
			{
				return facing > 0 ? _ColorFront : _ColorBlack;
			}

			ENDCG
		}
	}
}
