using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace UIControls.Controls
{
    public class RangeSlider : UserControl
    {
        double tmpLeftValue;
        double tmpRightValue;

        bool _ignoreUpdate = false;

        public override void OnApplyTemplate()
        {
            TmpFirstValue = FirstSliderValue;
            TmpSecondValue = SecondSliderValue;

            base.OnApplyTemplate();
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
            typeof(RangeSlider),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'LeftThumbValue'
        /// <summary>
        /// sets or gets the LeftThumbValue
        /// </summary>
        public double LeftThumbValue
        {
            get { return (double)this.GetValue(LeftThumbValueProperty); }
            set { this.SetValue(LeftThumbValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LeftThumbValue
        /// </summary>
        public static readonly DependencyProperty LeftThumbValueProperty = DependencyProperty.Register(
            "LeftThumbValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, LeftThumbValuePropertyChangedCallback)
        );
        private static void LeftThumbValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (!_this._ignoreUpdate)
                {
                    _this._ignoreUpdate = true;
                    if ((double)e.NewValue < _this.RightThumbValue)
                    {
                        _this.SecondSliderValue = _this.RightThumbValue;
                        _this.FirstSliderValue = (double)e.NewValue;
                    }
                    else
                    {
                        _this.FirstSliderValue = _this.RightThumbValue;
                        _this.SecondSliderValue = (double)e.NewValue;

                    }
                    _this._ignoreUpdate = false;
                }
            }
        }

        #endregion

        #region DependencyProperty 'RightThumbValue'
        /// <summary>
        /// sets or gets the RightThumbValue
        /// </summary>
        public double RightThumbValue
        {
            get { return (double)this.GetValue(RightThumbValueProperty); }
            set { this.SetValue(RightThumbValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty RightThumbValue
        /// </summary>
        public static readonly DependencyProperty RightThumbValueProperty = DependencyProperty.Register(
            "RightThumbValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, RightThumbValuePropertyChangedCallback)
        );
        private static void RightThumbValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (!_this._ignoreUpdate)
                {
                    _this._ignoreUpdate = true;
                    if ((double)e.NewValue < _this.LeftThumbValue)
                    {
                        _this.SecondSliderValue = _this.LeftThumbValue;
                        _this.FirstSliderValue = (double)e.NewValue;
                    }
                    else
                    {
                        _this.FirstSliderValue = _this.LeftThumbValue;
                        _this.SecondSliderValue = (double)e.NewValue;
                    }
                    _this._ignoreUpdate = false;
                }
            }
        }

        #endregion

        #region DependencyProperty 'FirstSliderValue'
        /// <summary>
        /// sets or gets the FirstSliderValue
        /// </summary>
        public double FirstSliderValue
        {
            get { return (double)this.GetValue(FirstSliderValueProperty); }
            set { this.SetValue(FirstSliderValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FirstSliderValue
        /// </summary>
        public static readonly DependencyProperty FirstSliderValueProperty = DependencyProperty.Register(
            "FirstSliderValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, FirstSliderValuePropertyChangedCallback)
        );
        private static void FirstSliderValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (!_this._ignoreUpdate)
                {
                    _this._ignoreUpdate = true;
                    if (_this.FirstSliderValue > _this.SecondSliderValue)
                    {
                        _this.LeftThumbValue = _this.SecondSliderValue;
                        _this.RightThumbValue = _this.FirstSliderValue;
                    }
                    else
                    {
                        _this.LeftThumbValue = _this.FirstSliderValue;
                        _this.RightThumbValue = _this.SecondSliderValue;
                    }
                    _this._ignoreUpdate = false;
                }
            }
        }

        #endregion

        #region DependencyProperty 'SecondSliderValue'
        /// <summary>
        /// sets or gets the SecondSliderValue
        /// </summary>
        public double SecondSliderValue
        {
            get { return (double)this.GetValue(SecondSliderValueProperty); }
            set { this.SetValue(SecondSliderValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SecondSliderValue
        /// </summary>
        public static readonly DependencyProperty SecondSliderValueProperty = DependencyProperty.Register(
            "SecondSliderValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, SecondSliderValuePropertyChangedCallback)
        );
        private static void SecondSliderValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (!_this._ignoreUpdate)
                {
                    _this._ignoreUpdate = true;
                    if (_this.SecondSliderValue < _this.FirstSliderValue)
                    {
                        _this.LeftThumbValue = _this.SecondSliderValue;
                        _this.RightThumbValue = _this.FirstSliderValue;
                    }
                    else
                    {
                        _this.LeftThumbValue = _this.FirstSliderValue;
                        _this.RightThumbValue = _this.SecondSliderValue;
                    }
                    _this._ignoreUpdate = false;
                }
            }
        }

        #endregion

        #region DependencyProperty 'TmpFirstValue'
        /// <summary>
        /// sets or gets the TmpFirstValue
        /// </summary>
        public double TmpFirstValue
        {
            get { return (double)this.GetValue(TmpFirstValueProperty); }
            set { this.SetValue(TmpFirstValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty TmpFirstValue
        /// </summary>
        public static readonly DependencyProperty TmpFirstValueProperty = DependencyProperty.Register(
            "TmpFirstValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, TmpFirstValuePropertyChangedCallback)
        );
        private static void TmpFirstValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (_this.ShowRoundedValues)
                {
                    _this.FirstShownValue = Math.Round(_this.TmpFirstValue, 0);
                }
                else
                {
                    _this.FirstShownValue = Math.Round(_this.TmpFirstValue, 2);
                }
            }
        }

        #endregion

        #region DependencyProperty 'TmpSecondValue'
        /// <summary>
        /// sets or gets the TmpSecondValue
        /// </summary>
        public double TmpSecondValue
        {
            get { return (double)this.GetValue(TmpSecondValueProperty); }
            set { this.SetValue(TmpSecondValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty TmpSecondValue
        /// </summary>
        public static readonly DependencyProperty TmpSecondValueProperty = DependencyProperty.Register(
            "TmpSecondValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, TmpSecondValuePropertyChangedCallback)
        );
        private static void TmpSecondValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {
                if (_this.ShowRoundedValues)
                {
                    _this.SecondShownValue = Math.Round(_this.TmpSecondValue, 0);
                }
                else
                {
                    _this.SecondShownValue = Math.Round(_this.TmpSecondValue, 2);
                }
            }
        }

        #endregion

        #region DependencyProperty 'FirstShownValue'
        /// <summary>
        /// sets or gets the FirstShownValue
        /// </summary>
        public double FirstShownValue
        {
            get { return (double)this.GetValue(FirstShownValueProperty); }
            set { this.SetValue(FirstShownValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FirstShownValue
        /// </summary>
        public static readonly DependencyProperty FirstShownValueProperty = DependencyProperty.Register(
            "FirstShownValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, FirstShownValuePropertyChangedCallback)
        );
        private static void FirstShownValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'SecondShownValue'
        /// <summary>
        /// sets or gets the SecondShownValue
        /// </summary>
        public double SecondShownValue
        {
            get { return (double)this.GetValue(SecondShownValueProperty); }
            set { this.SetValue(SecondShownValueProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SecondShownValue
        /// </summary>
        public static readonly DependencyProperty SecondShownValueProperty = DependencyProperty.Register(
            "SecondShownValue",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0, SecondShownValuePropertyChangedCallback)
        );
        private static void SecondShownValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RangeSlider _this = sender as RangeSlider;
            if (_this != null)
            {

            }
        }

        #endregion

        #region DependencyProperty 'FirstSliderMinimum'
        /// <summary>
        /// sets or gets the FirstSliderMinimum
        /// </summary>
        public double FirstSliderMinimum
        {
            get { return (double)this.GetValue(FirstSliderMinimumProperty); }
            set { this.SetValue(FirstSliderMinimumProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FirstSliderMinimum
        /// </summary>
        public static readonly DependencyProperty FirstSliderMinimumProperty = DependencyProperty.Register(
            "FirstSliderMinimum",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.0)
        );
        #endregion

        #region DependencyProperty 'FirstSliderMaximum'
        /// <summary>
        /// sets or gets the FirstSliderMaximum
        /// </summary>
        public double FirstSliderMaximum
        {
            get { return (double)this.GetValue(FirstSliderMaximumProperty); }
            set { this.SetValue(FirstSliderMaximumProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FirstSliderMaximum
        /// </summary>
        public static readonly DependencyProperty FirstSliderMaximumProperty = DependencyProperty.Register(
            "FirstSliderMaximum",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.5)
        );
        #endregion

        #region DependencyProperty 'SecondSliderMinimum'
        /// <summary>
        /// sets or gets the SecondSliderMinimum
        /// </summary>
        public double SecondSliderMinimum
        {
            get { return (double)this.GetValue(SecondSliderMinimumProperty); }
            set { this.SetValue(SecondSliderMinimumProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SecondSliderMinimum
        /// </summary>
        public static readonly DependencyProperty SecondSliderMinimumProperty = DependencyProperty.Register(
            "SecondSliderMinimum",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(0.5)
        );
        #endregion

        #region DependencyProperty 'SecondSliderMaximum'
        /// <summary>
        /// sets or gets the SecondSliderMaximum
        /// </summary>
        public double SecondSliderMaximum
        {
            get { return (double)this.GetValue(SecondSliderMaximumProperty); }
            set { this.SetValue(SecondSliderMaximumProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SecondSliderMaximum
        /// </summary>
        public static readonly DependencyProperty SecondSliderMaximumProperty = DependencyProperty.Register(
            "SecondSliderMaximum",
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(1.0)
        );
        #endregion

        #region DependencyProperty 'ShowRoundedValues'
        /// <summary>
        /// sets or gets the ShowRoundedValues
        /// </summary>
        public bool ShowRoundedValues
        {
        get { return (bool)this.GetValue(ShowRoundedValuesProperty); }
        set { this.SetValue(ShowRoundedValuesProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowRoundedValues
        /// </summary>
        public static readonly DependencyProperty ShowRoundedValuesProperty = DependencyProperty.Register(
            "ShowRoundedValues",
            typeof(bool),
            typeof(RangeSlider),
            new PropertyMetadata(false)
        );
        #endregion
    }
}
