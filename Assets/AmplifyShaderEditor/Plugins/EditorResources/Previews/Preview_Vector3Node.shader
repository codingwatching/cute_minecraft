Shader "Hidden/Vector3Node"
{
	Properties {
		_InputVector ("_InputVector", Vector) = (0,0,0,0)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Preview.cginc"
			#pragma vertex vert_img
			#pragma fragment frag

			float4 _InputVector;

			float4 frag( v2f_img i ) : SV_Target
			{
				return float4(_InputVector.x,_InputVector.y,_InputVector.z,0);
			}
			ENDCG
		}
	}
}
