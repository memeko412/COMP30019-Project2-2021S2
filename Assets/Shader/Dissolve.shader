
Shader "Custom/Dissolve"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_AddColor("AddColor",Color) = (0,0,0,0)
		[NoScaleOffset]_DissolveTex("DissolveTex", 2D) = "white" {}
		EdgeColor("EdgeColor",Color) = (0,0,0,0)
		_ClipAmount("ClipAmount",Range(0,1)) = 0
		_EdgeAmount("EdgeAmount",Range(0,0.2)) = 0.1

		_XRayColor("XRayColor",Color) = (0,0,0,0)
		_RimScale("RimScale",Float) = 1
		_RimPower("RimPower",Float) = 1
	}
	SubShader
	{
		//Dissolve
		Pass
		{
			Tags{"LightMode" = "ForwardBase"}


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_fwdbase 


			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal:NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldPos:TEXCOORD1;

				float3 worldViewDir:TEXCOORD2;
				float3 worldNormal :TEXCOORD3;
			};

			sampler2D _MainTex;	float4 _MainTex_ST;
			float4 _AddColor;
			sampler2D _DissolveTex;
			float4 EdgeColor;
			float _ClipAmount;
			float _EdgeAmount;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//normal direction
                float3 normal_dir = normalize(i.worldNormal);
				//light direction
                float3 light_dir = normalize(_WorldSpaceLightPos0.xyz);

				//Diffuse reflectance
                float diffuse = saturate(dot(normal_dir,light_dir)) * 0.5 + 0.3;

				//Sample color map
				float4 col = tex2D(_MainTex, i.uv) * diffuse;
					
				//Overlay additional colors. If there is no color map, use this color first
				col *= _AddColor;
					
				//Sample dissolving map
				fixed4 dis = tex2D(_DissolveTex, i.uv);

				//Clip (x) : Clip x less than 0. White is 1, black is 0, you look at the dissolve map. Clipamount is adjustable on the outside
				clip(dis.r - _ClipAmount);

				//Find the edge, 0 is edge, 1 is non-edge
				fixed disValue = saturate((dis.r - _ClipAmount) / _EdgeAmount);

				//Lerp (a,b,x): if x is 1, then B; if x is 0, then A
				col = lerp(EdgeColor, col,disValue);

				return col;
			}
			ENDCG
		}

		//XRay
		Pass
        {
			
            Tags{"Queue" = "Transparent"}

			//The color of this pass is superimposed on the first pass
            blend One One
			//Close ZWrite
            ZWrite Off
			//Pass when the depth is less than, that is, render the pass when occluded
            ZTest Greater

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                fixed3 normal:NORMAL;
				float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                fixed3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };


			float4 _XRayColor;
			float _RimScale;
			float _RimPower;
			sampler2D _DissolveTex;
			float _ClipAmount;

            v2f vert (appdata v)
            {
                v2f o;
				o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld,v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//view direction
                fixed3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
				//normal direction
                fixed3 N = normalize(i.worldNormal);
                //dot product of view direction and normal direction
                fixed VdotN = dot(V,N);

				//Fresnel
                fixed fresnel = _RimScale * pow(1- saturate( VdotN),_RimPower);

				//Sample dissolving map
				fixed4 dis = tex2D(_DissolveTex, i.uv);

				//Clip (x) : Clip x less than 0. White is 1, black is 0, you look at the dissolve map. Clipamount is adjustable on the outside
				clip(dis.r - _ClipAmount);

                return fresnel * _XRayColor ;
            }
            ENDCG
        }

		//Cast shadow, it's cast shadow and when it dissolves, the shadow dissolves with the first pass
		Pass
        {
            Tags{"LightMode" = "ShadowCaster"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            struct appdata
            {
              float4 vertex: POSITION;
              half3 normal:NORMAL;
			  float2 uv : TEXCOORD0;
            };

            struct v2f
            {
				float2 uv : TEXCOORD0;
                V2F_SHADOW_CASTER;
            };

			sampler2D _DissolveTex;float4 _DissolveTex_ST;
			float _ClipAmount;

            v2f vert(appdata v)
            {
                v2f o;
				o.uv = TRANSFORM_TEX(v.uv,_DissolveTex);

                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)

                return o;
            }

            fixed4 frag(v2f i):SV_Target
            {
				fixed4 dis = tex2D(_DissolveTex, i.uv);
				clip(dis.r - _ClipAmount);				

                SHADOW_CASTER_FRAGMENT(i)
				
				return 0;
            }

            ENDCG

        }
		}
}