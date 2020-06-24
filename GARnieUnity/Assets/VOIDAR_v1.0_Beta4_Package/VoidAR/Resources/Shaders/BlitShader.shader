Shader "Unlit/BlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_ScaleX("ScaleX", float) = 1.0
		_ScaleY("ScaleY", float) = 1.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			float _ScaleX;
			float _ScaleY;

            fixed4 frag (v2f i) : SV_Target
            {
				float4 color;
				float2 newUV;
                if( _ScaleX < 1.0 )
				{
					float xStart = 0.5 - _ScaleX / 2;
					float xEnd = 0.5 + _ScaleX / 2;
					if ( i.uv[0] <  xStart || i.uv[0] > xEnd  ){
						color =  (0,0,0,0);
					} else {
						newUV[1] = i.uv[1];
						newUV[0] = (i.uv[0] - xStart) / _ScaleX;
						color =  tex2D(_MainTex, newUV);
					}
				}
				else
				{
					float yStart = 0.5 - _ScaleY / 2;
					float yEnd = 0.5 + _ScaleY / 2;
					if ( i.uv[1] <  yStart || i.uv[1] > yEnd  ){
						color =  (0,0,0,0);
					} else {
						newUV[0] = i.uv[0];
						newUV[1] = (i.uv[1] - yStart) / _ScaleY;
						color =  tex2D(_MainTex, newUV);
					}
				}
				return color;
            }
            ENDCG
        }
    }
}
