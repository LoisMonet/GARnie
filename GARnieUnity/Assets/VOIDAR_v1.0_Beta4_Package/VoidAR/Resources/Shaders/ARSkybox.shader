Shader "Custom/ARSkybox"
{
	Properties
	{
		_LightingTex("Lighting Tex", 2D) = "white" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

	struct appdata
	{
		float4 position : POSITION;
		float3 normal : NORMAL;
		float3 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 position : SV_POSITION;
		float2 texcoord : TEXCOORD0;
	};

	sampler2D _LightingTex;
	float4x4 _WorldToCameraMatrix;

	float2 SphereMapUVCoords( float3 viewDir, float3 normal )
	{
		float3 reflection = reflect(viewDir, normal);
		float m = 2. * sqrt(
			pow(reflection.x, 2.) +
			pow(reflection.y, 2.) +
			pow(reflection.z + 1., 2.)
		);
		return reflection.xy / m + .5;
	}

	v2f vert(appdata v)
	{

		float3 viewDir = -normalize(WorldSpaceViewDir(v.position));
		viewDir = mul(_WorldToCameraMatrix, float4(viewDir,0));

		v2f o;
		o.position = UnityObjectToClipPos(v.position);
		o.texcoord = SphereMapUVCoords(viewDir, v.normal);

		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		return tex2D(_LightingTex, i.texcoord);
	}

	ENDCG

	SubShader
	{
		Tags{ "RenderType" = "Background" "Queue" = "Background" }
			Pass
		{
			ZWrite Off
			Cull Off
			Fog{ Mode Off }
			CGPROGRAM
#pragma fragmentoption ARB_precision_hint_fastest
#pragma vertex vert
#pragma fragment frag
			ENDCG
		}
	}
}