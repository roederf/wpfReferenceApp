/// <description>A simple color blending shader for WPF.</description>
/// <target>WPF</target>
/// <profile>ps_2_0</profile>

//-----------------------------------------------------------------------------
// Constants
//-----------------------------------------------------------------------------

/// <summary>The MousePosition.</summary>
/// <type>Point</type>
/// <defaultValue>0,0</defaultValue>
float2 Position : register(c0);

/// <summary>The Magnifying factor.</summary>
/// <type>Float</type>
/// <defaultValue>10</defaultValue>
float Factor : register(c1);

/// <summary>The Width of the image in pixel.</summary>
/// <type>Float</type>
/// <defaultValue>0</defaultValue>
float PixelWidth : register(c2);

/// <summary>The Height of the image in pixel.</summary>
/// <type>Float</type>
/// <defaultValue>0</defaultValue>
float PixelHeight : register(c3);

/// <summary>The Width and Height of the target.</summary>
/// <type>Float</type>
/// <defaultValue>0</defaultValue>
float TargetSize : register(c4);

//-----------------------------------------------------------------------------
// Samplers
//-----------------------------------------------------------------------------

/// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
/// <samplingMode>NearestNeighbor</samplingMode>
sampler2D Input : register(s0);

//-----------------------------------------------------------------------------
// Pixel Shader
//-----------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float blockSize = TargetSize / Factor;	// 320
	float scale = blockSize / PixelWidth;	// 320 / 912 => ca. 0.35 

	float r = PixelWidth / PixelHeight;
	
	float x = uv.x;
	float y = uv.y;
	
	float coordx = ((Position.x) / PixelWidth) + scale * (x - 0.5 * (TargetSize / PixelWidth));
	float coordy = ((Position.y) / PixelHeight) + scale * (y - 0.5 * (TargetSize / PixelHeight));
		
	float4 col = tex2D( Input, float2(coordx, coordy));

	float4 blend = 0.75;

	//int gx = (uv.x * TargetSize) % Factor;
	//int gy = (uv.y * TargetSize) % Factor;
	//if(gx < 1 || gy < 1)
	//{
	//	col = abs(col - blend);
	//}
	
    return col;
}
