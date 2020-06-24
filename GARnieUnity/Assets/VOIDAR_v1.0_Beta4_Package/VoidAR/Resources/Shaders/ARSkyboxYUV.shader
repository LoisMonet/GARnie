Shader "Custom/ARSkyboxYUV"
{
	Properties
	{
		Yp ("Y channel texture", 2D) = "white" {}
		CbCr("CbCr channel texture", 2D) = "black" {}
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

	sampler2D Yp;
	sampler2D CbCr;
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
		o.texcoord.y = 1.0 - o.texcoord.y;

		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		float3 yuv = float3(  
					1.1643 * (	tex2D(Yp, i.texcoord).r - 0.0625),  //y
					tex2D(CbCr, i.texcoord).a - 0.5,  //v
					tex2D(CbCr, i.texcoord).r - 0.5  //u
		    	);  


		float3x3 yuv2rgb = { 
						1, 0, 1.2802,  
					    1, -0.214821, -0.380589,  
					    1, 2.127982, 0  };


		float3 rgb = mul(yuv2rgb , yuv); 
		fixed4 color = float4(rgb, 1);
		return color;
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