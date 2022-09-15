﻿Shader "UniStorm/URP/UniStormCloudShadows"
{
	Properties
	{
		_MainTex("Color (RGB) Alpha (A)", 2D) = "white" { }
		_CloudShadowIntensity("Cloud Shadow Intensity", Range(0.0, 1.0)) = 1.0
		_CloudMovementSpeed("Cloud Movement Speed", Range(-0.5, 0.5)) = -0.06
	}
		SubShader
		{
			Cull Off ZWrite Off ZTest Always

			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_instancing
				#pragma enable_d3d11_debug_symbols

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;

					UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
					UNITY_VERTEX_OUTPUT_STEREO //Insert
				};

				v2f vert(appdata v)
				{
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v); //Insert
					UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;

					return o;
				}

				UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
				//sampler2D _MainTex;
				UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
				//sampler2D _CameraDepthNormalsTexture;
				float4x4 _CamToWorld;

				sampler2D _CloudTex;
				float _CloudTexScale;
				float _CloudShadowIntensity;
				float _CloudMovementSpeed;
				float _Fade;
				half  _normalY;

				half4 _ShadowColor;

				fixed _BottomThreshold;
				fixed _TopThreshold;

				uniform float4 _SunDirection;

				half4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); //Insert

					half3 normal;
					float depth;

					float2 offset = _Time.x * float2(_CloudMovementSpeed,0);

					//DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.uv), depth, normal);
					depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, UnityStereoTransformScreenSpaceTex(i.uv));

					normal = half3(0, _normalY, 0); //0.875
					//normal = mul((float3x3)_CamToWorld, normal);

					half ShadowThreshold = normal.g;
					half ShadowThreshold2 = normal.r;
					half scale = 1;

					float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
					float3 vpos = float3((i.uv * 2 - 1) / p11_22, -1) * depth;
					float4 wpos = mul(_CamToWorld, float4(vpos, 0));
					wpos += float4(_WorldSpaceCameraPos, 0) / _ProjectionParams.z - _SunDirection * 0.25;

					half4 ShadowColor = tex2D(_CloudTex, -wpos.xz * _CloudTexScale * 0.2 * (_ProjectionParams.z) + offset);

					float up = dot(float3(0, 1, 0), normal);
					up = step(ShadowThreshold * ShadowThreshold2, up);

					float4 source = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoTransformScreenSpaceTex(i.uv)); //tex2D
					half viewDist = length(vpos);
					half falloff = saturate((viewDist) / (_Fade));
					float4 col = lerp(source, source / ShadowColor, -up * _CloudShadowIntensity * ShadowThreshold * (1.0f - falloff));
					return col;
				}
				ENDCG
			}
		}
}
