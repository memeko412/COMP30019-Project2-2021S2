#include "Lighting.cginc"

Shader "Custom/ColorAdjustEffect"
{
	//shader attribute
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Brightness("Brightness", Float) = 1
		_Saturation("Saturation", Float) = 1
		_Contrast("Contrast", Float) = 1

		_RGBOffset("RGBOffset",Range(0,0.5)) = 0
		_ChromaticPower("ChromaticPower",Range(1,10)) = 1

	}


	SubShader
	{
		/*Rendering actually starts, one slice renders once, 
		which can be multiple passes, and n passes per slice renders n passes */
		Pass
			{		
				CGPROGRAM

				//Re-define shader attribut
				sampler2D _MainTex;
				half _Brightness;
				half _Saturation;
				half _Contrast;
				half _RGBOffset;
				half _ChromaticPower;

				//define name of functions
				#pragma vertex vert
				#pragma fragment frag

				//A structure that receives data from a vertex function
				struct v2f
				{
					float4 pos : SV_POSITION; 
					half2  uv : TEXCOORD0;	  
				};

				
				//vertex function
				v2f vert(appdata_img v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					return o;
				}

				//fragment function
				fixed4 frag(v2f i) : SV_Target
				{

					float3 col = tex2D(_MainTex, i.uv).rgb;
					
					//HSV Part
					//Brightness
					col = col * _Brightness;

					//Saturability
					fixed gray = 0.2125 * col.r + 0.7154 * col.g + 0.0721 * col.b;
					fixed3 grayColor = fixed3(gray, gray, gray);

					col = lerp(grayColor, col, _Saturation);

					//Contrast
					fixed3 avgColor = fixed3(0.5, 0.5, 0.5);

					col = lerp(avgColor, col, _Contrast);

					//final result
					return fixed4(col, 1);
				}
					ENDCG
		}

	}

	FallBack Off
}