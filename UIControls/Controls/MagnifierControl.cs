using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;

namespace UIControls.Controls
{
    public class MagnifierControl: Control
    {
        
        void updatePixelPosition(Point uv)
        {
            if (Source != null)
            {
                if (uv.X >= 0 && uv.Y >= 0 && uv.X <= 1 && uv.Y <= 1)
                {
                    double uvx = 0.5 + (uv.X - 0.5);
                    double uvy = 0.5 + (uv.Y - 0.5);
                    double px = uvx * (Source.PixelWidth-1);
                    double py = uvy * (Source.PixelHeight-1);
                    PixelPosition = new Point(px, py);
                }
            }
        }

        void updateWidthAndHeight(BitmapSource bmp)
        {
            if (bmp != null)
            {
                ImagePixelWidth = bmp.PixelWidth;
                ImagePixelHeight = bmp.PixelHeight;
            }
        }

        #region DependencyProperty 'TargetSize'
        /// <summary>
        /// sets or gets the TargetSize
        /// </summary>
        public double TargetSize
        {
        get { return (double)this.GetValue(TargetSizeProperty); }
        set { this.SetValue(TargetSizeProperty, value); }
        }
        /// <summary>
        /// DependencyProperty TargetSize
        /// </summary>
        public static readonly DependencyProperty TargetSizeProperty = DependencyProperty.Register(
            "TargetSize",
            typeof(double),
            typeof(MagnifierControl),
            new PropertyMetadata(320.0)
        );
        #endregion
        
        #region DependencyProperty 'ImagePixelWidth'
        /// <summary>
        /// sets or gets the ImagePixelWidth
        /// </summary>
        public double ImagePixelWidth
        {
            get { return (double)this.GetValue(ImagePixelWidthProperty); }
            set { this.SetValue(ImagePixelWidthProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ImagePixelWidth
        /// </summary>
        public static readonly DependencyProperty ImagePixelWidthProperty = DependencyProperty.Register(
            "ImagePixelWidth",
            typeof(double),
            typeof(MagnifierControl),
            new PropertyMetadata(0.0, ImagePixelWidthPropertyChangedCallback)
        );
        private static void ImagePixelWidthPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MagnifierControl _this = sender as MagnifierControl;
            if (_this != null)
            {

            }
        }

        #endregion
        
        #region DependencyProperty 'ImagePixelHeight'
        /// <summary>
        /// sets or gets the ImagePixelHeight
        /// </summary>
        public double ImagePixelHeight
        {
        get { return (double)this.GetValue(ImagePixelHeightProperty); }
        set { this.SetValue(ImagePixelHeightProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ImagePixelHeight
        /// </summary>
        public static readonly DependencyProperty ImagePixelHeightProperty = DependencyProperty.Register(
            "ImagePixelHeight",
            typeof(double),
            typeof(MagnifierControl),
            new PropertyMetadata(0.0)
        );
        #endregion
                
        #region DependencyProperty 'Source'
        /// <summary>
        /// sets or gets the Source
        /// </summary>
        public BitmapSource Source
        {
            get { return (BitmapSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Source
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(BitmapSource),
            typeof(MagnifierControl),
            new PropertyMetadata(null, SourcePropertyChangedCallback)
        );
        private static void SourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MagnifierControl _this = sender as MagnifierControl;
            if (_this != null)
            {
                _this.updateWidthAndHeight(e.NewValue as BitmapSource);
                _this.updatePixelPosition(_this.UVPosition);
            }
        }

        #endregion
        
        #region DependencyProperty 'UVPosition'
        /// <summary>
        /// sets or gets the UVPosition
        /// </summary>
        public Point UVPosition
        {
            get { return (Point)this.GetValue(UVPositionProperty); }
            set { this.SetValue(UVPositionProperty, value); }
        }
        /// <summary>
        /// DependencyProperty UVPosition
        /// </summary>
        public static readonly DependencyProperty UVPositionProperty = DependencyProperty.Register(
            "UVPosition",
            typeof(Point),
            typeof(MagnifierControl),
            new PropertyMetadata(new Point(), UVPositionPropertyChangedCallback)
        );
        private static void UVPositionPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MagnifierControl _this = sender as MagnifierControl;
            if (_this != null)
            {
                _this.updatePixelPosition((Point)e.NewValue);
            }
        }

        #endregion
        
        #region DependencyProperty 'PixelPosition'
        /// <summary>
        /// sets or gets the PixelPosition
        /// </summary>
        public Point PixelPosition
        {
            get { return (Point)this.GetValue(PixelPositionProperty); }
            set { this.SetValue(PixelPositionProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PixelPosition
        /// </summary>
        public static readonly DependencyProperty PixelPositionProperty = DependencyProperty.Register(
            "PixelPosition",
            typeof(Point),
            typeof(MagnifierControl),
            new PropertyMetadata(new Point(), PixelPositionPropertyChangedCallback)
        );
        private static void PixelPositionPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MagnifierControl _this = sender as MagnifierControl;
            if (_this != null)
            {

            }
        }

        #endregion
    }
}
