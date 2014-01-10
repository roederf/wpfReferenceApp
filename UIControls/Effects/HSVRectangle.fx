/// <description>A HSV Color Rectagle shader for WPF.</description>
/// <target>WPF</target>
/// <profile>ps_2_0</profile>

//-----------------------------------------------------------------------------
// Constants
//-----------------------------------------------------------------------------

/// <summary>The luminance offset.</summary>
/// <type>Float</type>
/// <defaultValue>1.0</defaultValue>
float Luminance : register(c0);

/// <summary>The Target Hue to be displayed in center. 0 - 1</summary>
/// <type>Float</type>
/// <defaultValue>0.5</defaultValue>
float Hue : register(c1);

/// <summary>The range to be displayed. 0 is single color, 1 is whole range</summary>
/// <type>Float</type>
/// <defaultValue>1.0</defaultValue>
float Range : register(c2);

//-----------------------------------------------------------------------------
// Samplers
//-----------------------------------------------------------------------------

/// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
/// <samplingMode>Auto</samplingMode>
sampler2D Input : register(s0);

//-----------------------------------------------------------------------------
// Pixel Shader
//-----------------------------------------------------------------------------

float3 HUEtoRGB(float H) : float3
{
  float R = abs(H * 6 - 3) - 1;
  float G = 2 - abs(H * 6 - 2);
  float B = 2 - abs(H * 6 - 4);
  return saturate(float3(R,G,B));
}

float3 HSVtoRGB(float3 HSV) : float3
{
  float3 RGB = HUEtoRGB(HSV.x);
  return ((RGB - 1) * HSV.y + 1) * HSV.z;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Source = tex2D( Input,uv);
	float4 Color = 0;

	float r = 1;
	float a = (uv.x * Range) + (Hue - 0.5 * Range);
	a = (a > 1) ? a - 1 : (a < 0) ? a + 1 : a;

	float3 c = HSVtoRGB(float3(a,r,Luminance));

	Color = float4(c.x,c.y,c.z,1);
	Color *= Source.a;

	return Color;
}
