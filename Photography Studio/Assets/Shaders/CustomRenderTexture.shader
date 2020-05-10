Shader "Photography Studio/GrayscaleExposure"
{
	Properties
	{
		_Tex("InputTex", 2D) = "white" {}
		_FilmSensitivity("Sensitivity", float) = 0.5
		_RedSensitivity("Red sensitivity", float) = 0.2989
		_GreenSensitivity("Green sensitivity", float) = 0.5870
		_BlueSensitivity("Blue sensitivity", float) = 0.1140
	}

	SubShader
	{
	   Lighting Off
	   Blend One Zero

	   Pass
		{
			CGPROGRAM
			#include "UnityCustomRenderTexture.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag
			#pragma target 3.0

			sampler2D _Tex;
			float _FilmSensitivity;
			float _RedSensitivity;
			float _GreenSensitivity;
			float _BlueSensitivity;
			//sampler2D _SelfTexture2D;

			float4 frag(v2f_customrendertexture IN) : COLOR
			{
				float4 originalCol = tex2D(_Tex, IN.localTexcoord.xy); 
				float4 buffer = tex2D(_SelfTexture2D, IN.localTexcoord.xy);

				//originalCol += buffer * 0.5f;

				float4 result = buffer + (originalCol * _FilmSensitivity * unity_DeltaTime.x);
				//float4 result = originalCol;

				float value = result.r * _RedSensitivity + result.g * _GreenSensitivity + result.b * _BlueSensitivity;
				
				float4 col;
				col.r = value;
				col.g = value;
				col.b = value;
				col.a = 1;
				return col;
			}
			ENDCG
		}
	}
}