using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace UIControls.Controls
{
    public class Viewport : ContentControl
    {
        protected override Size MeasureOverride(Size constraint)
        {
            Size result = constraint;

            if (Double.IsPositiveInfinity(constraint.Width) && Double.IsPositiveInfinity(constraint.Height))
            {
                result = new Size(1600, 1200);
            }

            if (result.Width > result.Height * Ratio)
            {
                result.Width = result.Height * Ratio;
            }
            else if (result.Height > result.Width / Ratio)
            {
                result.Height = result.Width / Ratio;
            }

            return result;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (result.Width > result.Height * Ratio)
            {
                result.Width = result.Height * Ratio;
            }
            else if (result.Height > result.Width / Ratio)
            {
                result.Height = result.Width / Ratio;
            }

            return result;
        }

        #region DependencyProperty 'Ratio'
        /// <summary>
        /// sets or gets the Ratio
        /// </summary>
        public double Ratio
        {
            get { return (double)this.GetValue(RatioProperty); }
            set { this.SetValue(RatioProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Ratio
        /// </summary>
        public static readonly DependencyProperty RatioProperty = DependencyProperty.Register(
            "Ratio",
            typeof(double),
            typeof(Viewport),
            new PropertyMetadata(4.0/3.0, RatioPropertyChangedCallback)
        );
        private static void RatioPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Viewport _this = sender as Viewport;
            if (_this != null)
            {

            }
        }

        #endregion
        
    }
}
