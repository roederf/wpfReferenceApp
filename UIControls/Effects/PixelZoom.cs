// -----------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by:
//        The WPF ShaderEffect Generator
//        http://wpfshadergenerator.codeplex.com
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace UIControls.Effects
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Media3D;
    
    
    /// <summary>A simple color blending shader for WPF.</summary>
    public class PixelZoom : System.Windows.Media.Effects.ShaderEffect
    {
        
        /// <summary>The MousePosition.</summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(System.Windows.Point), typeof(PixelZoom), new UIPropertyMetadata(new Point(0,0), PixelShaderConstantCallback(0)));
        /// <summary>The Magnifying factor.</summary>
        public static readonly DependencyProperty FactorProperty = DependencyProperty.Register("Factor", typeof(System.Single), typeof(PixelZoom), new UIPropertyMetadata((float)10, PixelShaderConstantCallback(1)));
        /// <summary>The Width of the image in pixel.</summary>
        public static readonly DependencyProperty PixelWidthProperty = DependencyProperty.Register("PixelWidth", typeof(System.Single), typeof(PixelZoom), new UIPropertyMetadata((float)0, PixelShaderConstantCallback(2)));
        /// <summary>The Height of the image in pixel.</summary>
        public static readonly DependencyProperty PixelHeightProperty = DependencyProperty.Register("PixelHeight", typeof(System.Single), typeof(PixelZoom), new UIPropertyMetadata((float)0, PixelShaderConstantCallback(3)));
        /// <summary>The Width and Height of the target.</summary>
        public static readonly DependencyProperty TargetSizeProperty = DependencyProperty.Register("TargetSize", typeof(System.Single), typeof(PixelZoom), new UIPropertyMetadata((float)0, PixelShaderConstantCallback(4)));
        /// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(PixelZoom), 0, SamplingMode.NearestNeighbor);
        
        public PixelZoom()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("pack://application:,,,/UIControls;component/Effects/PixelZoom.ps", UriKind.Absolute);
            this.PixelShader = pixelShader;
            this.UpdateShaderValue(PositionProperty);
            this.UpdateShaderValue(FactorProperty);
            this.UpdateShaderValue(PixelWidthProperty);
            this.UpdateShaderValue(PixelHeightProperty);
            this.UpdateShaderValue(TargetSizeProperty);
            this.UpdateShaderValue(InputProperty);
            this.DdxUvDdyUvRegisterIndex = -1;
        }
        
        /// <summary>The MousePosition.</summary>
        public virtual System.Windows.Point Position
        {
            get
            {
                return ((System.Windows.Point)(this.GetValue(PositionProperty)));
            }
            set
            {
                this.SetValue(PositionProperty, value);
            }
        }
        
        /// <summary>The Magnifying factor.</summary>
        public virtual float Factor
        {
            get
            {
                return ((float)(this.GetValue(FactorProperty)));
            }
            set
            {
                this.SetValue(FactorProperty, value);
            }
        }
        
        /// <summary>The Width of the image in pixel.</summary>
        public virtual float PixelWidth
        {
            get
            {
                return ((float)(this.GetValue(PixelWidthProperty)));
            }
            set
            {
                this.SetValue(PixelWidthProperty, value);
            }
        }
        
        /// <summary>The Height of the image in pixel.</summary>
        public virtual float PixelHeight
        {
            get
            {
                return ((float)(this.GetValue(PixelHeightProperty)));
            }
            set
            {
                this.SetValue(PixelHeightProperty, value);
            }
        }
        
        /// <summary>The Width and Height of the target.</summary>
        public virtual float TargetSize
        {
            get
            {
                return ((float)(this.GetValue(TargetSizeProperty)));
            }
            set
            {
                this.SetValue(TargetSizeProperty, value);
            }
        }
        
        /// <summary>The implicit input sampler passed into the pixel shader by WPF.</summary>
        public virtual System.Windows.Media.Brush Input
        {
            get
            {
                return ((System.Windows.Media.Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
    }
}
