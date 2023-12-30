Shader "Unlit/laserShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
      _NoiseTex ("Noise Texture", 2D) = "white" {}
      _Mitigation("Noise mitigatiojn", Range(0,1))=0.5
      _LineWidth("Laser Width", Range(0,1))=0.5
      _Speed("Speed", Range(0,100))=0.5

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
         sampler2D _NoiseTex;
			float4 _MainTex_ST;
            float _LineWidth;
            float _Speed;
			float _Mitigation;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
                float2 uv= i.uv;
                uv.x+=_Time;
                fixed4 col = tex2D(_MainTex, i.uv);

             float noise = tex2D(_NoiseTex, i.uv + _Time.y * _Speed).r * _Mitigation;
                fixed4 color = (step(0.5 - _LineWidth, i.uv.y + noise) * step(i.uv.y + noise, 0.5 + _LineWidth)) * col;
                return color;
            //fixed4 noise = tex2D(_NoiseTex, uv);


				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
               if(i.uv.y<0.5-(_LineWidth*noise.r)|| i.uv.y >0.5+_LineWidth)
               discard;
				return color*col;
			}
			ENDCG
		}
	}
}
