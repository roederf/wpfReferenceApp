using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace UIControls.Controls
{
    public class AnalogAxisControl : Control
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Slider slider = GetTemplateChild("PART_Slider") as Slider;
            if (slider != null)
            {
                slider.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(slider_PreviewMouseLeftButtonUp);
                slider.PreviewTouchUp += new EventHandler<TouchEventArgs>(slider_PreviewTouchUp);
            }
        }

        void slider_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            Value = LowerLimit + (UpperLimit - LowerLimit) * 0.5;
        }

        void slider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Reset();   
        }
                        
        #region DependencyProperty 'Orientation'
        /// <summary>
        /// sets or gets the Orientation
        /// </summary>
        public Orientation Orientation
        {
        get { return (Orientation)this.GetValue(OrientationProperty); }
        set { this.SetValue(OrientationProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Orientation
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(AnalogAxisControl),
            new PropertyMetadata(Orientation.Vertical)
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
            typeof(AnalogAxisControl),
            new PropertyMetadata(0.0, ValuePropertyChangedCallback)
        );
        private static void ValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AnalogAxisControl _this = sender as AnalogAxisControl;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'UpperLimit'
        /// <summary>
        /// sets or gets the UpperLimit
        /// </summary>
        public double UpperLimit
        {
            get { return (double)this.GetValue(UpperLimitProperty); }
            set { this.SetValue(UpperLimitProperty, value); }
        }
        /// <summary>
        /// DependencyProperty UpperLimit
        /// </summary>
        public static readonly DependencyProperty UpperLimitProperty = DependencyProperty.Register(
            "UpperLimit",
            typeof(double),
            typeof(AnalogAxisControl),
            new PropertyMetadata(1.0, UpperLimitPropertyChangedCallback)
        );
        private static void UpperLimitPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AnalogAxisControl _this = sender as AnalogAxisControl;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'LowerLimit'
        /// <summary>
        /// sets or gets the LowerLimit
        /// </summary>
        public double LowerLimit
        {
            get { return (double)this.GetValue(LowerLimitProperty); }
            set { this.SetValue(LowerLimitProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LowerLimit
        /// </summary>
        public static readonly DependencyProperty LowerLimitProperty = DependencyProperty.Register(
            "LowerLimit",
            typeof(double),
            typeof(AnalogAxisControl),
            new PropertyMetadata(-1.0, LowerLimitPropertyChangedCallback)
        );
        private static void LowerLimitPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AnalogAxisControl _this = sender as AnalogAxisControl;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'IncreaseCommand'
        /// <summary>
        /// sets or gets the IncreaseCommand
        /// </summary>
        public ICommand IncreaseCommand
        {
        get { return (ICommand)this.GetValue(IncreaseCommandProperty); }
        set { this.SetValue(IncreaseCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IncreaseCommand
        /// </summary>
        public static readonly DependencyProperty IncreaseCommandProperty = DependencyProperty.Register(
            "IncreaseCommand",
            typeof(ICommand),
            typeof(AnalogAxisControl),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'DecreaseCommand'
        /// <summary>
        /// sets or gets the DecreaseCommand
        /// </summary>
        public ICommand DecreaseCommand
        {
        get { return (ICommand)this.GetValue(DecreaseCommandProperty); }
        set { this.SetValue(DecreaseCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty DecreaseCommand
        /// </summary>
        public static readonly DependencyProperty DecreaseCommandProperty = DependencyProperty.Register(
            "DecreaseCommand",
            typeof(ICommand),
            typeof(AnalogAxisControl),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'SliderDirectionReversed'
        /// <summary>
        /// sets or gets the Value
        /// </summary>
        public bool SliderDirectionReversed
        {
            get { return (bool)this.GetValue(SliderDirectionReversedProperty); }
            set { this.SetValue(SliderDirectionReversedProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Value
        /// </summary>
        public static readonly DependencyProperty SliderDirectionReversedProperty = DependencyProperty.Register(
            "SliderDirectionReversed",
            typeof(bool),
            typeof(AnalogAxisControl),
            new PropertyMetadata(false, SliderDirectionReversedPropertyChangedCallback)
        );
        private static void SliderDirectionReversedPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AnalogAxisControl _this = sender as AnalogAxisControl;
            if (_this != null)
            {

            }
        }

        #endregion
        
    }
}
