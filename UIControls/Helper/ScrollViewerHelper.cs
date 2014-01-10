using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UIControls.Helper
{
    public class ScrollViewerHelper : DependencyObject
    {
        #region AttachedDependencyProperty 'BoundaryFeedbackFix'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetBoundaryFeedbackFix(ScrollViewer element, bool value)
        {
            element.SetValue(BoundaryFeedbackFixProperty, value);
        }
        public static bool GetBoundaryFeedbackFix(ScrollViewer element)
        {
            return (bool)element.GetValue(BoundaryFeedbackFixProperty);
        }

        /// <summary>
        /// DependencyProperty BoundaryFeedbackFix
        /// </summary>
        public static readonly DependencyProperty BoundaryFeedbackFixProperty = DependencyProperty.RegisterAttached(
            "BoundaryFeedbackFix",
            typeof(bool),
            typeof(ScrollViewerHelper),
            new PropertyMetadata(false, BoundaryFeedbackFixPropertyChangedCallback)
        );
        private static void BoundaryFeedbackFixPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                var s = sender as ScrollViewer;
                s.ManipulationBoundaryFeedback += s_ManipulationBoundaryFeedback;
            }
        }

        static void s_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        #endregion
        
    }
}
