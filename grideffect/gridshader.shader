﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/gridshader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_Amount ("Amount",Range(0,1))=1

		_GridType("GridType",Range(1,2))=1

		_GridColor("GridColor",Color)=(0,0,0,1)

		_Xstep("Xstep",Range(1,10))=1
		_Ystep("Ystep",Range(1,10))=1

		_Xoffset("Xoffset",Range(0,10))=0
		_Yoffset("Yoffset",Range(0,10))=0

		_Xmin1("Xmin",Range(0,1))=0
		_Xmax1("Xmax",Range(0,1))=1

		_Ymin1("Ymin",Range(0,1))=0
		_Ymax1("Ymax",Range(0,1))=1

		_Flashing("Flashing",Range(0,1))=0
		_Flashcount("Flashcount",Range(0,1))=0

		_LineSizeX("LineSizeX",Range(0,100)) = 0
		_LineSizeY("LineSizeY",Range(0,100)) = 0
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
			float _Amount;

			uint _GridType;

			fixed4 _GridColor;

			uint _Xstep;
			uint _Ystep;

			uint _Xoffset;
			uint _Yoffset;

			float _Xmin;
			float _Xmax;
			float _Ymin;
			float _Ymax;

			uint _Flashing;
			uint _Flashcount;

			uint _LineSizeX;
			uint _LineSizeY;

			float amo2;
			
			
			fixed4 col;
			fixed4 frag (v2f i) : SV_Target
			{
				col = tex2D(_MainTex, i.uv);
				
				// just invert the colors
				//col = 1 - col;
				//return col;

				uint yzure=0;

				float jjj=i.uv.x;
				float bbb=i.uv.y;
				fixed4 gridc=_GridColor;

				if(_Xmin<jjj && _Xmax>jjj && _Ymin<bbb && _Ymax>bbb)
				{
					jjj=jjj*_ScreenParams.xy.x;
					int jj2=int(jjj);
					//jj2-=_Xoffset;
					jj2+=_Xoffset;
					bbb=bbb*_ScreenParams.xy.y;
					int bb2=int(bbb);
					bb2+=_Yoffset;
					bb2 += _Flashcount/4;

					//reverse
					//amo2=1-_Amount;
					amo2=_Amount;

					int t = _LineSizeX;
					if(
						jj2%(_Xstep) < _LineSizeX
						)
					{

						float xx = ((float)(jj2 % (_Xstep))) / (_LineSizeX)- 0.5;

						if (xx < 0) {
							xx =- xx;
						}
						xx = .5	 - xx;
						//if(_Xint>1)col-=fixed4(_Amount,_Amount,_Amount,0);
						//if(_Xstep>1)col=fixed4(col.r*amo2,col.g*amo2,col.b*amo2,0);

						if(_Xstep>1)
						{
							fixed4 vec33=fixed4((gridc.r-col.r)*amo2,(gridc.g-col.g)*amo2,(gridc.b-col.b)*amo2, gridc.a);
							col=fixed4(
								col.r+vec33.r * vec33.a * xx,
								col.g+vec33.g * vec33.a * xx,
								col.b+vec33.b * vec33.a * xx,
								1
							);
						}
					}
					else if(_GridType==2 && _Xstep>1 && _Ystep>1)
					{
						if((jj2 + (_Xstep *2))%_Xstep >= _LineSizeX)yzure=int((_Ystep)/2);
						bb2+=yzure;
					}
					{
						if(bb2%_Ystep < _LineSizeY
							//&& jj2 % (_Xstep) >= _LineSizeX
							)
						{
							//if(_Yint>1)col-=fixed4(_Amount,_Amount,_Amount,0);
							//if(_Ystep>1)col=fixed4(col.r*amo2,col.g*amo2,col.b*amo2,0);

							float yy = ((float)(bb2 % (_Ystep))) / (_LineSizeY)-0.5;

							if (yy < 0) {
								yy = -yy;
							}
							yy = .5 - yy;
							if(_Ystep>1)
							{
								fixed4 vec33 = fixed4((gridc.r - col.r) * amo2, (gridc.g - col.g) * amo2, (gridc.b - col.b) * amo2, gridc.a);
								col = fixed4(
									col.r + vec33.r * vec33.a * yy,
									col.g + vec33.g * vec33.a * yy,
									col.b + vec33.b * vec33.a * yy,
									1
								);
							}
						}
					}
				}


				/*if(_Flashcount==1)
				{
					col=fixed4(col.r*0.5,col.g*0.5,col.b*0.5,1);
				}*/
				//col = fixed4(1, 1, 0, 1);
				return col;
			}
			ENDCG
		}
	}
}
