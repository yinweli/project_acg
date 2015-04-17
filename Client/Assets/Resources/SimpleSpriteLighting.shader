Shader "Custom/SimpleSpriteLighting"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"RenderType"="Transparent"
		}
		LOD 200
		
		Cull Off
		Blend One OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		void vert(inout appdata_full v, out Input o)
		{
			v.normal = float3(0, 0, -1);
		
			UNITY_INITIALIZE_OUTPUT(Input, o);
			
			o.color = v.color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * IN.color;
			
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
