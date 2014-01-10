using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using UIControls.Utilities;

namespace UIControls.Controls
{
    public class ColorPicker : Control
    {
        private FrameworkElement _circle;
        private Slider _brightnessSlider; 
        private Thumb _thumb;
        private Point _CurrentPickPosition;
        private TouchPoint _CurrentTouchPosition;
        private double[] _HSVColor = new double[3] { 1, 1, 1 };

        private bool _colorByPicking = false;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _circle = this.GetTemplateChild("PART_Circle") as FrameworkElement;
            if (_circle != null)
            {
                _circle.MouseLeftButtonDown += new MouseButtonEventHandler(_circle_MouseLeftButtonDown);
                _circle.MouseLeftButtonUp += new MouseButtonEventHandler(_circle_MouseLeftButtonUp);
                _circle.TouchDown += _circle_TouchDown;
                _circle.TouchUp += _circle_TouchUp;
            }

            _thumb = this.GetTemplateChild("PART_Thumb") as Thumb;

            _brightnessSlider = this.GetTemplateChild("PART_BrightnessSlider") as Slider;
            if (_brightnessSlider != null)
            {
                _brightnessSlider.PreviewMouseLeftButtonUp += _brightnessSlider_PreviewMouseLeftButtonUp;
            }

            this.SizeChanged += ColorPicker_SizeChanged;
        }

        void _circle_TouchDown(object sender, TouchEventArgs e)
        {
            //_CurrentTouchPosition = e.GetTouchPoint(_circle);
            
            //_colorByPicking = true;
            //InternalPickColor = PickColor = CalculateColorFromCoordinates(_CurrentTouchPosition.Position.X / _circle.ActualWidth, _CurrentTouchPosition.Position.Y / _circle.ActualHeight);
            //_colorByPicking = false;

            //ThumbPositionX = _CurrentPickPosition.X - _circle.ActualWidth / 2;
            //ThumbPositionY = _CurrentPickPosition.Y - _circle.ActualHeight / 2;

            e.Handled = true;

            //if (_circle != null)
            //{
            //    _circle.TouchMove += _circle_TouchMove;
            //}
        }

        void _circle_TouchMove(object sender, TouchEventArgs e)
        {
           
        }

        void _circle_TouchUp(object sender, TouchEventArgs e)
        {
            //if (_circle != null) _circle.TouchMove -= _circle_TouchMove;

            //_CurrentTouchPosition = e.GetTouchPoint(_circle);

            //_colorByPicking = true;
            //InternalPickColor = PickColor = CalculateColorFromCoordinates(_CurrentTouchPosition.Position.X / _circle.ActualWidth, _CurrentTouchPosition.Position.Y / _circle.ActualHeight);
            //_colorByPicking = false;

            e.Handled = true;

            //this.RaiseEvent(new ColorPickedEventArgs(PickColor));
        }

        void _brightnessSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Slider s = sender as Slider;
            UpdateValue((double)s.Value);
        }

        void ColorPicker_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateFromColor();
        }

        void _circle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _CurrentPickPosition = new Point((int)Mouse.GetPosition(_circle).X, (int)Mouse.GetPosition(_circle).Y);

            _colorByPicking = true;
            InternalPickColor = PickColor = CalculateColorFromCoordinates(_CurrentPickPosition.X / _circle.ActualWidth, _CurrentPickPosition.Y / _circle.ActualHeight);
            _colorByPicking = false;

            ThumbPositionX = _CurrentPickPosition.X - _circle.ActualWidth / 2;
            ThumbPositionY = _CurrentPickPosition.Y - _circle.ActualHeight / 2;

            _circle.CaptureMouse();

            if (_circle != null)
            {
                _circle.MouseMove += new System.Windows.Input.MouseEventHandler(_circle_MouseMove);
            }
        }

        void _circle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_circle != null) _circle.MouseMove -= new System.Windows.Input.MouseEventHandler(_circle_MouseMove);

            _CurrentPickPosition = new Point((int)Mouse.GetPosition(_circle).X, (int)Mouse.GetPosition(_circle).Y);
            
            _colorByPicking = true;
            InternalPickColor = PickColor = CalculateColorFromCoordinates(_CurrentPickPosition.X / _circle.ActualWidth, _CurrentPickPosition.Y / _circle.ActualHeight);
            _colorByPicking = false;

            e.Handled = true;
            _circle.ReleaseMouseCapture();

            this.RaiseEvent(new ColorPickedEventArgs(PickColor));
        }

        void _circle_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _CurrentPickPosition = new Point((int)Mouse.GetPosition(_circle).X, (int)Mouse.GetPosition(_circle).Y);

            _colorByPicking = true;
            InternalPickColor = CalculateColorFromCoordinates(_CurrentPickPosition.X / _circle.ActualWidth, _CurrentPickPosition.Y / _circle.ActualHeight);
            if (!this.IgnoreMoveUpdates)
            {
                PickColor = InternalPickColor;
            }
            _colorByPicking = false;

            double[] radangle = CalculateAngleAndRadiusFromCoordinates(_CurrentPickPosition.X / _circle.ActualWidth, _CurrentPickPosition.Y / _circle.ActualHeight);

            if (radangle[0] < 1)
            {
                ThumbPositionX = _CurrentPickPosition.X - _circle.ActualWidth / 2;
                ThumbPositionY = _CurrentPickPosition.Y - _circle.ActualHeight / 2;
            }
        }

        Double[] CalculateAngleAndRadiusFromCoordinates(double x, double y)
        {
            Double deltah = Math.Abs((x - 0.5) * (x - 0.5));
            Double deltav = Math.Abs((y - 0.5) * (y - 0.5));
            Double delta = Math.Sqrt(deltah + deltav) * 2;

            Double angle = Math.Atan2(x - 0.5, y - 0.5) / 6.28 + 0.5;

            return new double[2] { delta, angle };
        }

        Color CalculateColorFromCoordinates(double x, double y)
        {
            Color col = new Color();

            Double deltah = Math.Abs((x - 0.5) * (x - 0.5));
            Double deltav = Math.Abs((y - 0.5) * (y - 0.5));
            Double delta = Math.Sqrt(deltah + deltav) * 2;

            delta = (delta > 1) ? 1 : delta;

            Double angle = Math.Atan2(x - 0.5, y - 0.5) / 6.28 + 0.5;

            _HSVColor = new double[3] { angle, delta, Value };

            PickHue = angle;

            col = ColorUtilities.HSVtoRGB(_HSVColor);

            return col;
        }

        void UpdateValue(double v)
        {
            _HSVColor = new double[3] { _HSVColor[0], _HSVColor[1], v };
            PickColor = ColorUtilities.HSVtoRGB(_HSVColor);
            this.RaiseEvent(new ColorPickedEventArgs(PickColor));
        }

        private void UpdateFromColor()
        {
            InternalPickColor = PickColor;
            _HSVColor = ColorUtilities.RGBToHSV(PickColor);

            if (_circle != null)
            {
                Point p = ColorUtilities.colorToUV(_HSVColor);

                ThumbPositionX = p.X * _circle.ActualWidth;
                ThumbPositionY = p.Y * _circle.ActualHeight;
            }

            _HSVColor = ColorUtilities.RGBToHSV(PickColor);
            PickHue = _HSVColor[0];
            Value = _HSVColor[2];
        }

        #region DependencyProperty 'IgnoreMoveUpdates'
        /// <summary>
        /// sets or gets the IgnoreMoveUpdates
        /// </summary>
        public bool IgnoreMoveUpdates
        {
        get { return (bool)this.GetValue(IgnoreMoveUpdatesProperty); }
        set { this.SetValue(IgnoreMoveUpdatesProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IgnoreMoveUpdates
        /// </summary>
        public static readonly DependencyProperty IgnoreMoveUpdatesProperty = DependencyProperty.Register(
            "IgnoreMoveUpdates",
            typeof(bool),
            typeof(ColorPicker),
            new PropertyMetadata(false)
        );
        #endregion
        
        #region DependencyProperty 'ThumbPosition'
        /// <summary>
        /// sets or gets the ThumbPosition
        /// </summary>
        public Point ThumbPosition
        {
            get { return (Point)this.GetValue(ThumbPositionProperty); }
            set { this.SetValue(ThumbPositionProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ThumbPosition
        /// </summary>
        public static readonly DependencyProperty ThumbPositionProperty = DependencyProperty.Register(
            "ThumbPosition",
            typeof(Point),
            typeof(ColorPicker),
            new PropertyMetadata(new Point())
        );
        #endregion

        #region DependencyProperty 'ThumbPositionX'
        /// <summary>
        /// sets or gets the ThumbPositionX
        /// </summary>
        public double ThumbPositionX
        {
            get { return (double)this.GetValue(ThumbPositionXProperty); }
            set { this.SetValue(ThumbPositionXProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ThumbPositionX
        /// </summary>
        public static readonly DependencyProperty ThumbPositionXProperty = DependencyProperty.Register(
            "ThumbPositionX",
            typeof(double),
            typeof(ColorPicker),
            new PropertyMetadata(0.0)
        );
        #endregion

        #region DependencyProperty 'ThumbPositionY'
        /// <summary>
        /// sets or gets the ThumbPositionY
        /// </summary>
        public double ThumbPositionY
        {
            get { return (double)this.GetValue(ThumbPositionYProperty); }
            set { this.SetValue(ThumbPositionYProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ThumbPositionY
        /// </summary>
        public static readonly DependencyProperty ThumbPositionYProperty = DependencyProperty.Register(
            "ThumbPositionY",
            typeof(double),
            typeof(ColorPicker),
            new PropertyMetadata(0.0)
        );
        #endregion

        #region DependencyProperty 'PickColor'
        /// <summary>
        /// sets or gets the PickColor
        /// </summary>
        public Color PickColor
        {
            get { return (Color)this.GetValue(PickColorProperty); }
            set { this.SetValue(PickColorProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PickColor
        /// </summary>
        public static readonly DependencyProperty PickColorProperty = DependencyProperty.Register(
            "PickColor",
            typeof(Color),
            typeof(ColorPicker),
            new PropertyMetadata(Colors.White, PickColorPropertyChangedCallback)
        );
        private static void PickColorPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker _this = sender as ColorPicker;
            if (_this != null)
            {
                if (!_this._colorByPicking)
                {
                    _this.UpdateFromColor();
                }
            }
        }

        #endregion

        #region DependencyProperty 'InternalPickColor'
        /// <summary>
        /// sets or gets the InternalPickColor
        /// </summary>
        public Color InternalPickColor
        {
        get { return (Color)this.GetValue(InternalPickColorProperty); }
        set { this.SetValue(InternalPickColorProperty, value); }
        }
        /// <summary>
        /// DependencyProperty InternalPickColor
        /// </summary>
        public static readonly DependencyProperty InternalPickColorProperty = DependencyProperty.Register(
            "InternalPickColor",
            typeof(Color),
            typeof(ColorPicker),
            new PropertyMetadata(Colors.White, InternalPickColorPropertyChangedCallback)
        );
        private static void InternalPickColorPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Object _this = sender as Object;
            if (_this != null)
            {

            }
        }
        #endregion
        
        #region DependencyProperty 'PickHue'
        /// <summary>
        /// sets or gets the PickHue
        /// </summary>
        public double PickHue
        {
            get { return (double)this.GetValue(PickHueProperty); }
            set { this.SetValue(PickHueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PickHue
        /// </summary>
        public static readonly DependencyProperty PickHueProperty = DependencyProperty.Register(
            "PickHue",
            typeof(double),
            typeof(ColorPicker),
            new PropertyMetadata(0.0)
        );
        #endregion

        #region DependencyProperty 'Value'
        /// <summary>
        /// sets or gets the Value
        /// </summary>
        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Value
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(ColorPicker),
            new PropertyMetadata(1.0, ValuePropertyChangedCallback)
        );
        private static void ValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker _this = sender as ColorPicker;
            if (_this != null)
            {
                if (!_this.IgnoreMoveUpdates)
                {
                    _this.UpdateValue((double)e.NewValue);
                }
            }
        }
        #endregion

        #region DependencyProperty 'PickCommand'
        /// <summary>
        /// sets or gets the PickCommand
        /// </summary>
        public ICommand PickCommand
        {
            get { return (ICommand)this.GetValue(PickCommandProperty); }
            set { this.SetValue(PickCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PickCommand
        /// </summary>
        public static readonly DependencyProperty PickCommandProperty = DependencyProperty.Register(
            "PickCommand",
            typeof(ICommand),
            typeof(ColorPicker),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'PickCommandParameter'
        /// <summary>
        /// sets or gets the PickCommandParameter
        /// </summary>
        public object PickCommandParameter
        {
            get { return (object)this.GetValue(PickCommandParameterProperty); }
            set { this.SetValue(PickCommandParameterProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PickCommandParameter
        /// </summary>
        public static readonly DependencyProperty PickCommandParameterProperty = DependencyProperty.Register(
            "PickCommandParameter",
            typeof(object),
            typeof(ColorPicker),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'ShowBucket'
        /// <summary>
        /// sets or gets the ShowBucket
        /// </summary>
        public bool ShowBucket
        {
            get { return (bool)this.GetValue(ShowBucketProperty); }
            set { this.SetValue(ShowBucketProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowBucket
        /// </summary>
        public static readonly DependencyProperty ShowBucketProperty = DependencyProperty.Register(
            "ShowBucket",
            typeof(bool),
            typeof(ColorPicker),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'ShowDropper'
        /// <summary>
        /// sets or gets the ShowDropper
        /// </summary>
        public bool ShowDropper
        {
            get { return (bool)this.GetValue(ShowDropperProperty); }
            set { this.SetValue(ShowDropperProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowDropper
        /// </summary>
        public static readonly DependencyProperty ShowDropperProperty = DependencyProperty.Register(
            "ShowDropper",
            typeof(bool),
            typeof(ColorPicker),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'ShowSelectedColor'
        /// <summary>
        /// sets or gets the ShowSelectedColor
        /// </summary>
        public bool ShowSelectedColor
        {
            get { return (bool)this.GetValue(ShowSelectedColorProperty); }
            set { this.SetValue(ShowSelectedColorProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowSelectedColor
        /// </summary>
        public static readonly DependencyProperty ShowSelectedColorProperty = DependencyProperty.Register(
            "ShowSelectedColor",
            typeof(bool),
            typeof(ColorPicker),
            new PropertyMetadata(true)
        );
        #endregion

        #region DependencyProperty 'ShowThresholdSlider'
        /// <summary>
        /// sets or gets the ShowThresholdSlider
        /// </summary>
        public bool ShowThresholdSlider
        {
            get { return (bool)this.GetValue(ShowThresholdSliderProperty); }
            set { this.SetValue(ShowThresholdSliderProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowThresholdSlider
        /// </summary>
        public static readonly DependencyProperty ShowThresholdSliderProperty = DependencyProperty.Register(
            "ShowThresholdSlider",
            typeof(bool),
            typeof(ColorPicker),
            new PropertyMetadata(false)
        );
        #endregion

        #region RoutedEvent 'ColorPicked'
        public class ColorPickedEventArgs : RoutedEventArgs
        {
            public ColorPickedEventArgs(Color c)
            {
                this.RoutedEvent = ColorPickedEvent;
                PickedColor = c;
            }

            public Color PickedColor { get; private set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public delegate void ColorPickedEventHandler(object sender, ColorPickedEventArgs e);

        /// <summary>
        /// RoutedEvent ColorPickedEvent
        /// </summary>
        public static readonly RoutedEvent ColorPickedEvent = EventManager.RegisterRoutedEvent(
            "ColorPicked",
            RoutingStrategy.Bubble,
            typeof(ColorPickedEventHandler),
            typeof(ColorPicker)
            );
        #endregion
    }
}
