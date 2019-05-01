Shader "Custom/Water/FoamedWater"
{
	Properties
	{
		_WaterColor ("Water color", Color) = (0.008, 0.239, 0.745, 1)
		_WaterTex ("Water texture", 2D) = "white" {}
		_Tiling ("Water tiling", Vector) = (1, 1, 1, 1)
		_TextureVisibility("Texture visibility", Range(0, 1)) = 1

		// Sway
		[Space(20)]
		_SwayFrequency("Sway Frequency", Float) = 1
		_SwayIntensity("Sway Intensity", Range(0, 1.0)) = 0.5

		// Flow
		[Space(20)]
		_FlowMovementV("Flow Movement Vertical", Range(0, 1.0)) = 0.1
		_FlowMovementH("Flow Movement Horizontal", Range(0, 1.0)) = 0.1
		_ScaleFlow("Scale Flow With Object", Range(0,1)) = 10
		_FlowMap("Flow map", 2D) = "white" {}
		_FlowSpeed("Flow Speed", Float) = 10
		_FlowPower("Flow Power", Float) = 0.5
		_TilingDifference("Goal Tiling Difference", Float) = 1.4

		// Depth & Foam
		[Space(20)]
		_Foam("Foam", 2D) = "white" {}
		_FoamTimeSpeed("Foam Time Speed", Range(0, 1.0)) = 0
		_FoamSpeedV("Foam Vertical Speed", Float) = 0
		_FoamSpeedH("Foam Horizontal Speed", Float) = 0
		_FoamSpread("FoamSpread", Range(0, 1.0)) = 1.0
		_Depth("Depth", Float) = 0
		_FoamSpreadV("Foam Vertical Spread", Float) = 0
		_FoamSpreadH("Foam Horizontal Spread", Float) = 0
		_FoamFalloff("Foam Falloff", Float) = 0.2
		_OpacityMinimum("Opacity Minimum", Range(0, 1.0)) = 0.25
		_EdgeLineWidth("Edge-Line Width", Float) = 0
		_FpamOutlineFalloff("Foam Outline Falloff", Float) = 1.0
		_FoamIntensity("Foam Intensity", Range(0, 2.0)) = 1
        
		[Space(20)]
        _WaveFrequency ("Wave frequency", Float) = 1
        _WaveAmplitude ("Wave amplitude", Float) = 1
        _WaveSize ("Wave size", Float) = 1
        
		[Space(20)]
		_DistTex ("Distortion", 2D) = "white" {}
		_DistTiling ("Distortion tiling", Vector) = (1, 1, 1, 1)

		[Space(20)]
		_WaterHeight ("Water height", Float) = 0

		[Space(20)]
		_MoveDirection ("Direction", Vector) = (0, 0, 0, 0)
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
			
			#include "UnityCG.cginc"
			#pragma multi_compile_fog
			#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON

			uniform float _FoamStrength;
			uniform sampler2D _CameraDepthTexture; //Depth Texture

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 worldPos: TEXCOORD1;
				fixed camHeightOverWater : TEXCOORD2;
				float2 foamuv : TEXCOORD4;
				UNITY_FOG_COORDS(3)
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _WaterTex;
			fixed2 _Tiling;
			fixed4 _WaterColor;
            
            fixed _WaveFrequency;
            fixed _WaveAmplitude;
            fixed _WaveSize;

			// Sway
			fixed _SwayFrequency;
			fixed _SwayIntensity;

			// Flow
			fixed _FlowMovementV;
			fixed _FlowMovementH;
			fixed _ScaleFlow;
			sampler2D _FlowMap;
			fixed _FlowSpeed;
			fixed _FlowPower;
			fixed _TilingDifference;

			// Depth & Foam
			sampler2D _Foam;
			fixed _FoamTimeSpeed;
			fixed _FoamSpeedV;
			fixed _FoamSpeedH;
			fixed _FoamSpread;
			fixed _Depth;
			fixed _FoamSpreadV;
			fixed _FoamSpreadH;
			fixed _FoamFalloff;
			fixed _OpacityMinimum;
			fixed _EdgeLineWidth;
			fixed _FpamOutlineFalloff;
			fixed _FoamIntensity;

			sampler2D _DistTex;
			fixed2 _DistTiling;

			fixed _WaterHeight;
			fixed _TextureVisibility;

			fixed3 _MoveDirection;

			fixed2 WaterPlaneUV(fixed3 worldPos, fixed camHeightOverWater)
			{
                fixed adjustedCamHeight = fixed(camHeightOverWater + 0.3 * sin((worldPos.x / 10) + _Time.x * 50));
				//fixed3 camToWorldRay = fixed3(worldPos.x, worldPos.y + sin((worldPos.x) * _Time.x / 10), worldPos.z) - _WorldSpaceCameraPos;
                fixed3 camToWorldRay = worldPos - _WorldSpaceCameraPos;
				fixed3 rayToWaterPlane = (adjustedCamHeight / camToWorldRay.y * camToWorldRay);
                //fixed3 rayToWaterPlane = (camHeightOverWater / camToWorldRay.y * camToWorldRay);
				return rayToWaterPlane.xz - _WorldSpaceCameraPos.xz;
			}
            
            //float3 add;
            
			v2f vert (appdata v)
			{
				v2f o;
                //add = float4(0f, 0f, _SinTime.w, 0f);
				o.worldPos = mul(UNITY_MATRIX_M, v.vertex);
				o.vertex = mul(UNITY_MATRIX_VP, o.worldPos);
                //o.vertex += sin(_Time * o.vertex.x);
				
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.camHeightOverWater = _WorldSpaceCameraPos.y - _WaterHeight;

				#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
					fixed3 camToWorldRay = o.worldPos - _WorldSpaceCameraPos;
					fixed3 rayToWaterPlane = (o.camHeightOverWater / camToWorldRay.y * camToWorldRay);

					fixed3 worldPosOnPlane = _WorldSpaceCameraPos - rayToWaterPlane;
                
					fixed3 positionForFog = lerp(worldPosOnPlane, o.worldPos.xyz, o.worldPos.y > _WaterHeight);
					fixed4 waterVertex = mul(UNITY_MATRIX_VP, fixed4(positionForFog, 1));
					UNITY_TRANSFER_FOG(o, waterVertex);
				#endif

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed st = _SwayIntensity * sin(_SwayFrequency * 6.283185 * _Time.x) + _Time.x;

				fixed2 water_uv = WaterPlaneUV(i.worldPos, i.camHeightOverWater);
                
				fixed4 distortion = tex2D(_DistTex, water_uv * _DistTiling) * 2 - 1;
				fixed2 distorted_uv = ((water_uv + distortion.rg) - _Time.y * _MoveDirection.xz) * _Tiling;

				fixed4 waterCol = tex2D(_WaterTex, distorted_uv);
                
				waterCol = lerp(_WaterColor, fixed4(1, 1, 1, 1), waterCol.r * _TextureVisibility);
                fixed3 ray = i.worldPos - _WorldSpaceCameraPos;
                fixed len = length(ray);
                fixed3 a = ray / len;
                fixed val = 1 / (1 + exp(-20 * a.y));
                //waterCol += lerp(fixed4(1, 1, 1, 1), fixed4(0, 0, 0, 1), clamp(abs(val), 0, 1));
                waterCol += lerp(fixed4(0, 0, 0, 1), fixed4(1, 1, 1, 1), clamp(abs(val), 0, 1));

				// Draw foamuv
				

				//UNITY_APPLY_FOG(i.fogCoord, waterCol);

				return waterCol;
			}
			ENDCG
		}
	}
}
