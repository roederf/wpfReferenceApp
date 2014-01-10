/// <description>A HSV Color Circle shader for WPF.</description>
/// <target>WPF</target>
/// <profile>ps_2_0</profile>

//-----------------------------------------------------------------------------
// Constants
//-----------------------------------------------------------------------------

/// <summary>The luminance offset.</summary>
/// <type>Float</type>
/// <defaultValue>1</defaultValue>
float Value : register(c0);

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

	float hsquare = abs((uv.x-0.5)*(uv.x-0.5));
	float vsquare = abs((uv.y-0.5)*(uv.y-0.5));
	float r = sqrt( vsquare + hsquare ) * 2;
	float a = atan2(uv.x-0.5, uv.y-0.5) / 6.28 + 0.5;
	float3 c = HSVtoRGB(float3(a,r,Value));

	Color = float4(c.x,c.y,c.z,1);
	Color *= Source.a;

	return Color;
}