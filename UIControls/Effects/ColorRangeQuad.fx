/// <description>A HSV Color Quad shader for WPF.</description>
/// <target>WPF</target>
/// <profile>ps_2_0</profile>

//-----------------------------------------------------------------------------
// Constants
//-----------------------------------------------------------------------------

/// <summary>The range input.</summary>
/// <type>Float</type>
/// <defaultValue>1</defaultValue>
float4 InputColor : register(c0);

/// <summary>The color input.</summary>
/// <type>Float</type>
/// <defaultValue>0</defaultValue>
float Range : register(c1);

//-----------------------------------------------------------------------------
// Samplers
//-----------------------------------------------------------------------------

/// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
/// <samplingMode>Auto</samplingMode>
sampler2D Input : register(s0);

//-----------------------------------------------------------------------------
// Pixel Shader
//-----------------------------------------------------------------------------
float4 RGBtoHSV(float3 RGB) : float4
{
	float3 HSV = 0;
	HSV.z = max(RGB.x, max(RGB.y, RGB.z));
	float M = min(RGB.x, min(RGB.y, RGB.z));
	float C = HSV.z - M;
	if (C != 0)
	{
		HSV.y = C / HSV.z;
		float3 Delta = (HSV.z - RGB) / C;
		Delta.rgb -= Delta.brg;
		Delta.rg += float2(2,4);
		if (RGB.r >= HSV.z)
			HSV.x = Delta.b;
		else if (RGB.g >= HSV.z)
			HSV.x = Delta.r;
		else
			HSV.x = Delta.g;
			HSV.x = frac(HSV.x / 6);
	}
	return float4(HSV.x,HSV.y,HSV.z,1);
}

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

  float4 HSV = RGBtoHSV(float3(InputColor.r, InputColor.g, InputColor.b));

  float newH = HSV.x + ((uv.x - 0.5) * Range);
  float3 newRGB = HSVtoRGB(float3(newH, HSV.y, HSV.z));

  Color = float4(newRGB.x, newRGB.y, newRGB.z, 1);

  return Color;
}