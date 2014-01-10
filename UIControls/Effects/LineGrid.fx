/// <description>A simple color blending shader for WPF.</description>
/// <target>WPF</target>
/// <profile>ps_2_0</profile>

//-----------------------------------------------------------------------------
// Constants
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Samplers
//-----------------------------------------------------------------------------

/// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
/// <samplingMode>Auto</samplingMode>
sampler2D Input : register(s0);

//-----------------------------------------------------------------------------
// Pixel Shader
//-----------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Source = tex2D( Input, uv);
	float4 col = 0;

	float uvx = (uv.x * 300) % 10;
	float uvy = (uv.y * 300) % 10;

	if(uvx < 1 || uvy < 1)
	{
		col = float4(1,1,1,1);
		col *= 0.75;
	}

    return col;
}
