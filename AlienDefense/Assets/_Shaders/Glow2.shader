Shader "Unlit/Glow2"{
	Properties {
		_Color ("Coklor",Color ) = (1,1,1,1)
		_Size("Size", Range(0,16))=4
		_Rim("Fade",Range(0,8))=4
	}
	SubShader{
		Tags {"RenderType"="Transparent"}
		LOD 200

		Cull Front

		CGPROGRAM
		#pragma surface surf Lambert fullforwardshadows alpha:fade
		#pragma vertex vert

	struct Input 
	{float3 viewDir;};

	half _Size;
	half _Rim;
	fixed4 _Color;

	void vert (inout appdata_full v) {
		v.vertex.xyz += v.vertex.xyz * _Size / 10;
		v.normal *= -1;
	}

	void surf( Input IN, inout SurfaceOutput o ){
		half rim = saturate(dot(normalize (IN.viewDir),normalize(o.Normal)));

		fixed4 c = _Color;
		o.Emission = c.rgb;
		o.Alpha = lerp (0,1 ,pow(rim,_Rim));
	}
	ENDCG
}
}