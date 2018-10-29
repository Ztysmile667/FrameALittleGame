// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Simple" 
{
	Properties
	{
		_MainTex("Base(RGB)",2D) = "white"{}
		_Color("Base Color",Color) = (1,1,1,1)
	}
	SubShader
	{
		tags{"Queue" = "TransParent"  "RenderType" = "TransParent"  "IgnoreProjector" = "True"}
		
		Blend SrcAlpha OneMinusSrcAlpha

		pass
		{
			Name"Simple"
			Cull off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _Color;

			struct v2f
			{
				float4 pos:POSITION;
				float4 uv:TEXCOORDO;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			half4 frag(v2f i):COLOR
			{
				half4 c = text2D(_MainTex,i.uv.xy) * _Color;
				return c;
			}
			ENDCG
		}
	}
}
