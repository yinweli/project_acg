﻿Shader "Custom/MyDiffuse"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha	

	// ---- forward rendering base pass:
	Pass {
		Name "FORWARD"
		Tags
		{
			"Queue" ="Overlay" 
			"LightMode" = "ForwardBase"
		}

	CGPROGRAM
	// compile directives
	#pragma vertex vert_surf
	#pragma fragment frag_surf
	#pragma multi_compile _ PIXELSNAP_ON
	#pragma multi_compile_fwdbase
	#include "HLSLSupport.cginc"
	#include "UnityShaderVariables.cginc"
	#if !defined(PIXELSNAP_ON)
	#define UNITY_PASS_FORWARDBASE
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"

	#define INTERNAL_DATA
	#define WorldReflectionVector(data,normal) data.worldRefl
	#define WorldNormalVector(data,normal) normal

	// Original surface shader snippet:
	#line 23 ""
	#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
	#endif

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input
			{
				float2 uv_MainTex;
				fixed4 color;
			};
		
			void vert (inout appdata_full v, out Input o)
			{
				#if defined(PIXELSNAP_ON)
				v.vertex = UnityPixelSnap (v.vertex);
				#endif
			
				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.color = v.color * _Color;
			}

			void surf (Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
				o.Albedo = c.rgb * c.a;
				o.Alpha = c.a;
			}

	// vertex-to-fragment interpolation data
	// ----------------------------------------------
	// no lightmaps:
	#ifdef LIGHTMAP_OFF
		struct v2f_surf {
		  float4 pos : SV_POSITION;
		  float2 pack0 : TEXCOORD0; // _MainTex
		  half3 worldNormal : TEXCOORD1;
		  float3 worldPos : TEXCOORD2;
		  half4 custompack0 : TEXCOORD3; // color
		  #if UNITY_SHOULD_SAMPLE_SH
		  half3 sh : TEXCOORD4; // SH
		  #endif
		  SHADOW_COORDS(5)
		  #if SHADER_TARGET >= 30
		  float4 lmap : TEXCOORD6;
		  #endif
		};
	#endif
	// with lightmaps:
	#ifndef LIGHTMAP_OFF
		struct v2f_surf {
		  float4 pos : SV_POSITION;
		  float2 pack0 : TEXCOORD0; // _MainTex
		  half3 worldNormal : TEXCOORD1;
		  float3 worldPos : TEXCOORD2;
		  half4 custompack0 : TEXCOORD3; // color
		  float4 lmap : TEXCOORD4;
		  SHADOW_COORDS(5)
		  #ifdef DIRLIGHTMAP_COMBINED
		  fixed3 tSpace0 : TEXCOORD6;
		  fixed3 tSpace1 : TEXCOORD7;
		  fixed3 tSpace2 : TEXCOORD8;
		  #endif
		};
	#endif
	// ----------------------------------------------
	float4 _MainTex_ST;

	// vertex shader
	v2f_surf vert_surf (appdata_full v) 
	{
		v2f_surf o;
		UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
		Input customInputData;
		vert (v, customInputData);
		o.custompack0.xyzw = customInputData.color;
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
		float3 worldPos = mul(_Object2World, v.vertex).xyz;
		fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
		
		#if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_COMBINED)
		fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
		fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
		#endif
		
		#if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_COMBINED)
		o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
		o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
		o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
		#endif
		
		o.worldPos = worldPos;
		o.worldNormal = worldNormal;
		
		#ifndef DYNAMICLIGHTMAP_OFF
		o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
		#endif
		
		#ifndef LIGHTMAP_OFF
		o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
		#endif

		  // SH/ambient and vertex lights
		  #ifdef LIGHTMAP_OFF
			#if UNITY_SHOULD_SAMPLE_SH
			  #if UNITY_SAMPLE_FULL_SH_PER_PIXEL
				o.sh = 0;
			  #elif (SHADER_TARGET < 30)
				o.sh = ShadeSH9 (float4(worldNormal,1.0));
			  #else
				o.sh = ShadeSH3Order (half4(worldNormal, 1.0));
			  #endif
			  // Add approximated illumination from non-important point lights
			  #ifdef VERTEXLIGHT_ON
				o.sh += Shade4PointLights (
				  unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
				  unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
				  unity_4LightAtten0, worldPos, worldNormal);
			  #endif
			#endif
		  #endif // LIGHTMAP_OFF

		  TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
		  return o;
	}

	// fragment shader
	fixed4 frag_surf (v2f_surf IN) : SV_Target {
	  // prepare and unpack data
	  Input surfIN;
	  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
	  surfIN.uv_MainTex = IN.pack0.xy;
	  surfIN.color = IN.custompack0.xyzw;
	  float3 worldPos = IN.worldPos;
	  #ifndef USING_DIRECTIONAL_LIGHT
		fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
	  #else
		fixed3 lightDir = _WorldSpaceLightPos0.xyz;
	  #endif
	  #ifdef UNITY_COMPILER_HLSL
	  SurfaceOutput o = (SurfaceOutput)0;
	  #else
	  SurfaceOutput o;
	  #endif
	  o.Albedo = 0.0;
	  o.Emission = 0.0;
	  o.Specular = 0.0;
	  o.Alpha = 0.0;
	  o.Gloss = 0.0;
	  fixed3 normalWorldVertex = fixed3(0,0,1);
	  o.Normal = IN.worldNormal;
	  normalWorldVertex = IN.worldNormal;

	  // call surface function
	  surf (surfIN, o);

	  // compute lighting & shadowing factor
	  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
	  fixed4 c = 0;

	  // Setup lighting environment
	  UnityGI gi;
	  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
	  gi.indirect.diffuse = 0;
	  gi.indirect.specular = 0;
	  #if !defined(LIGHTMAP_ON)
		  gi.light.color = _LightColor0.rgb;
		  gi.light.dir = lightDir;
		  gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
	  #endif
	  // Call GI (lightmaps/SH/reflections) lighting function
	  UnityGIInput giInput;
	  UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
	  giInput.light = gi.light;
	  giInput.worldPos = worldPos;
	  giInput.atten = atten;
	  #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
		giInput.lightmapUV = IN.lmap;
	  #else
		giInput.lightmapUV = 0.0;
	  #endif
	  #if UNITY_SHOULD_SAMPLE_SH
		giInput.ambient = IN.sh;
	  #else
		giInput.ambient.rgb = 0.0;
	  #endif
	  giInput.probeHDR[0] = unity_SpecCube0_HDR;
	  giInput.probeHDR[1] = unity_SpecCube1_HDR;
	  #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
		giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
	  #endif
	  #if UNITY_SPECCUBE_BOX_PROJECTION
		giInput.boxMax[0] = unity_SpecCube0_BoxMax;
		giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
		giInput.boxMax[1] = unity_SpecCube1_BoxMax;
		giInput.boxMin[1] = unity_SpecCube1_BoxMin;
		giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
	  #endif
	  LightingLambert_GI(o, giInput, gi);

	  // realtime lighting: call lighting function
	  c += LightingLambert (o, gi);
	  return c;
	}
	#endif

	// -------- variant for: PIXELSNAP_ON 
	#if defined(PIXELSNAP_ON)
	#define UNITY_PASS_FORWARDBASE
	#include "UnityCG.cginc"
	#include "Lighting.cginc"
	#include "AutoLight.cginc"

	#define INTERNAL_DATA
	#define WorldReflectionVector(data,normal) data.worldRefl
	#define WorldNormalVector(data,normal) normal

	// Original surface shader snippet:
	#line 23 ""
	#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
	#endif

			//#pragma surface surf Lambert vertex:vert nofog keepalpha
			//#pragma multi_compile _ PIXELSNAP_ON

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input
			{
				float2 uv_MainTex;
				fixed4 color;
			};
		
			void vert (inout appdata_full v, out Input o)
			{
				#if defined(PIXELSNAP_ON)
				v.vertex = UnityPixelSnap (v.vertex);
				#endif
			
				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.color = v.color * _Color;
			}

			void surf (Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
				o.Albedo = c.rgb * c.a;
				o.Alpha = c.a;
			}
		

	// vertex-to-fragment interpolation data
	// no lightmaps:
	#ifdef LIGHTMAP_OFF
	struct v2f_surf {
	  float4 pos : SV_POSITION;
	  float2 pack0 : TEXCOORD0; // _MainTex
	  half3 worldNormal : TEXCOORD1;
	  float3 worldPos : TEXCOORD2;
	  half4 custompack0 : TEXCOORD3; // color
	  #if UNITY_SHOULD_SAMPLE_SH
	  half3 sh : TEXCOORD4; // SH
	  #endif
	  SHADOW_COORDS(5)
	  #if SHADER_TARGET >= 30
	  float4 lmap : TEXCOORD6;
	  #endif
	};
	#endif
	// with lightmaps:
	#ifndef LIGHTMAP_OFF
	struct v2f_surf {
	  float4 pos : SV_POSITION;
	  float2 pack0 : TEXCOORD0; // _MainTex
	  half3 worldNormal : TEXCOORD1;
	  float3 worldPos : TEXCOORD2;
	  half4 custompack0 : TEXCOORD3; // color
	  float4 lmap : TEXCOORD4;
	  SHADOW_COORDS(5)
	  #ifdef DIRLIGHTMAP_COMBINED
	  fixed3 tSpace0 : TEXCOORD6;
	  fixed3 tSpace1 : TEXCOORD7;
	  fixed3 tSpace2 : TEXCOORD8;
	  #endif
	};
	#endif
	float4 _MainTex_ST;

	// vertex shader
	v2f_surf vert_surf (appdata_full v) {
	  v2f_surf o;
	  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
	  Input customInputData;
	  vert (v, customInputData);
	  o.custompack0.xyzw = customInputData.color;
	  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
	  float3 worldPos = mul(_Object2World, v.vertex).xyz;
	  fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
	  #if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_COMBINED)
	  fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
	  fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
	  #endif
	  #if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_COMBINED)
	  o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
	  o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
	  o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
	  #endif
	  o.worldPos = worldPos;
	  o.worldNormal = worldNormal;
	  #ifndef DYNAMICLIGHTMAP_OFF
	  o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
	  #endif
	  #ifndef LIGHTMAP_OFF
	  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
	  #endif

	  // SH/ambient and vertex lights
	  #ifdef LIGHTMAP_OFF
		#if UNITY_SHOULD_SAMPLE_SH
		  #if UNITY_SAMPLE_FULL_SH_PER_PIXEL
			o.sh = 0;
		  #elif (SHADER_TARGET < 30)
			o.sh = ShadeSH9 (float4(worldNormal,1.0));
		  #else
			o.sh = ShadeSH3Order (half4(worldNormal, 1.0));
		  #endif
		  // Add approximated illumination from non-important point lights
		  #ifdef VERTEXLIGHT_ON
			o.sh += Shade4PointLights (
			  unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
			  unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
			  unity_4LightAtten0, worldPos, worldNormal);
		  #endif
		#endif
	  #endif // LIGHTMAP_OFF

	  TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
	  return o;
	}

	// fragment shader
	fixed4 frag_surf (v2f_surf IN) : SV_Target {
	  // prepare and unpack data
	  Input surfIN;
	  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
	  surfIN.uv_MainTex = IN.pack0.xy;
	  surfIN.color = IN.custompack0.xyzw;
	  float3 worldPos = IN.worldPos;
	  #ifndef USING_DIRECTIONAL_LIGHT
		fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
	  #else
		fixed3 lightDir = _WorldSpaceLightPos0.xyz;
	  #endif
	  #ifdef UNITY_COMPILER_HLSL
	  SurfaceOutput o = (SurfaceOutput)0;
	  #else
	  SurfaceOutput o;
	  #endif
	  o.Albedo = 0.0;
	  o.Emission = 0.0;
	  o.Specular = 0.0;
	  o.Alpha = 0.0;
	  o.Gloss = 0.0;
	  fixed3 normalWorldVertex = fixed3(0,0,1);
	  o.Normal = IN.worldNormal;
	  normalWorldVertex = IN.worldNormal;

	  // call surface function
	  surf (surfIN, o);

	  // compute lighting & shadowing factor
	  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
	  fixed4 c = 0;

	  // Setup lighting environment
	  UnityGI gi;
	  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
	  gi.indirect.diffuse = 0;
	  gi.indirect.specular = 0;
	  #if !defined(LIGHTMAP_ON)
		  gi.light.color = _LightColor0.rgb;
		  gi.light.dir = lightDir;
		  gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
	  #endif
	  // Call GI (lightmaps/SH/reflections) lighting function
	  UnityGIInput giInput;
	  UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
	  giInput.light = gi.light;
	  giInput.worldPos = worldPos;
	  giInput.atten = atten;
	  #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
		giInput.lightmapUV = IN.lmap;
	  #else
		giInput.lightmapUV = 0.0;
	  #endif
	  #if UNITY_SHOULD_SAMPLE_SH
		giInput.ambient = IN.sh;
	  #else
		giInput.ambient.rgb = 0.0;
	  #endif
	  giInput.probeHDR[0] = unity_SpecCube0_HDR;
	  giInput.probeHDR[1] = unity_SpecCube1_HDR;
	  #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
		giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
	  #endif
	  #if UNITY_SPECCUBE_BOX_PROJECTION
		giInput.boxMax[0] = unity_SpecCube0_BoxMax;
		giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
		giInput.boxMax[1] = unity_SpecCube1_BoxMax;
		giInput.boxMin[1] = unity_SpecCube1_BoxMin;
		giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
	  #endif
	  LightingLambert_GI(o, giInput, gi);

	  // realtime lighting: call lighting function
	  c += LightingLambert (o, gi);
	  return c;
	}


	#endif


	ENDCG

	}

	// ---- forward rendering additive lights pass:
	Pass {
		Name "FORWARD"
		Tags
		{
			"Queue" = "Geometry" 
			"LightMode" = "ForwardAdd"
		}
		ZWrite Off Blend One One

CGPROGRAM
// compile directives
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile_fwdadd
#include "HLSLSupport.cginc"
#include "UnityShaderVariables.cginc"
// -------- variant for: <when no other keywords are defined>
#if !defined(PIXELSNAP_ON)
#define UNITY_PASS_FORWARDADD
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal

// Original surface shader snippet:
#line 23 ""
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		

// vertex-to-fragment interpolation data
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0; // _MainTex
  half3 worldNormal : TEXCOORD1;
  float3 worldPos : TEXCOORD2;
  half4 custompack0 : TEXCOORD3; // color
  SHADOW_COORDS(4)
};
float4 _MainTex_ST;

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xyzw = customInputData.color;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  float3 worldPos = mul(_Object2World, v.vertex).xyz;
  fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
  o.worldPos = worldPos;
  o.worldNormal = worldNormal;

  TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
  return o;
}

// fragment shader
fixed4 frag_surf (v2f_surf IN) : SV_Target {
  // prepare and unpack data
  Input surfIN;
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.uv_MainTex = IN.pack0.xy;
  surfIN.color = IN.custompack0.xyzw;
  float3 worldPos = IN.worldPos;
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutput o = (SurfaceOutput)0;
  #else
  SurfaceOutput o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);
  o.Normal = IN.worldNormal;
  normalWorldVertex = IN.worldNormal;

  // call surface function
  surf (surfIN, o);
  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
  fixed4 c = 0;

  // Setup lighting environment
  UnityGI gi;
  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
  gi.indirect.diffuse = 0;
  gi.indirect.specular = 0;
  #if !defined(LIGHTMAP_ON)
      gi.light.color = _LightColor0.rgb;
      gi.light.dir = lightDir;
      gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
  #endif
  gi.light.color *= atten;
  c += LightingLambert (o, gi);
  return c;
}


#endif

// -------- variant for: PIXELSNAP_ON 
#if defined(PIXELSNAP_ON)
#define UNITY_PASS_FORWARDADD
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal

// Original surface shader snippet:
#line 23 ""
#ifdef DUMMY_PREPROCESSOR_TO_WORK_AROUND_HLSL_COMPILER_LINE_HANDLING
#endif

		//#pragma surface surf Lambert vertex:vert nofog keepalpha
		//#pragma multi_compile _ PIXELSNAP_ON

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		

// vertex-to-fragment interpolation data
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0; // _MainTex
  half3 worldNormal : TEXCOORD1;
  float3 worldPos : TEXCOORD2;
  half4 custompack0 : TEXCOORD3; // color
  SHADOW_COORDS(4)
};
float4 _MainTex_ST;

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xyzw = customInputData.color;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  float3 worldPos = mul(_Object2World, v.vertex).xyz;
  fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
  o.worldPos = worldPos;
  o.worldNormal = worldNormal;

  TRANSFER_SHADOW(o); // pass shadow coordinates to pixel shader
  return o;
}

// fragment shader
fixed4 frag_surf (v2f_surf IN) : SV_Target {
  // prepare and unpack data
  Input surfIN;
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.uv_MainTex = IN.pack0.xy;
  surfIN.color = IN.custompack0.xyzw;
  float3 worldPos = IN.worldPos;
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutput o = (SurfaceOutput)0;
  #else
  SurfaceOutput o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);
  o.Normal = IN.worldNormal;
  normalWorldVertex = IN.worldNormal;

  // call surface function
  surf (surfIN, o);
  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
  fixed4 c = 0;

  // Setup lighting environment
  UnityGI gi;
  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
  gi.indirect.diffuse = 0;
  gi.indirect.specular = 0;
  #if !defined(LIGHTMAP_ON)
      gi.light.color = _LightColor0.rgb;
      gi.light.dir = lightDir;
      gi.light.ndotl = LambertTerm (o.Normal, gi.light.dir);
  #endif
  gi.light.color *= atten;
  c += LightingLambert (o, gi);
  return c;
}


#endif


ENDCG

}

	}

Fallback "Transparent/VertexLit"
}
