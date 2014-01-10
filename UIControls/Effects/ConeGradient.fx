/// <summary>First Color.</summary>
/// <defaultValue>White</defaultValue>
/// <type>Color</type>
float4 Color1: register(C1);

/// <summary>Second Color.</summary>
/// <defaultValue>Black</defaultValue>
/// <type>Color</type>
float4 Color2: register(C2);

/// <summary>Relative Angle offset between 0 and 1.</summary>
/// <defaultValue>0.0</defaultValue>
/// <type>float</type>
float Angle:register(C3);

/// <summary>blabla</summary>
/// <defaultValue>0.0</defaultValue>
/// <type>float</type>
sampler2D implicitInputSampler : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
 float4 Source = tex2D( implicitInputSampler, uv );
 float4 Color = 0;
 
 if(Source.a > 0){
	 float4 c1 = Color1 * Color1.a;
	 float4 c2 = Color2 * Color2.a;

	 float a = Angle;

	 float r = atan2(uv.x-0.5, uv.y-0.5)/6.28 + 0.5 + a;
	 r = r - (floor(r));

	 if (r<0.005) r = (0.005) + (0.005-r)*200;

	 float s = 0.7 - r;

	 Color = c1*r + c2*s;
	 Color *= Source.a;
 }
 
 return Color;
 
}