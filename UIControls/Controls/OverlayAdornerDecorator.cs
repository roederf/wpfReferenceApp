using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace UIControls.Controls
{
    [TemplatePart(Name = "PART_AdornerDecorator", Type = typeof(AdornerDecorator))]
    public class OverlayAdornerDecorator : ContentControl
    {
        private int _adornersCount;

        /// <summary>
        /// Initializes the <see cref="OverlayAdornerDecorator"/> class.
        /// </summary>
        //static OverlayAdornerDecorator()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(OverlayAdornerDecorator), new FrameworkPropertyMetadata(typeof(OverlayAdornerDecorator)));
        //}

        private AdornerDecorator _adornerDecorator;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _adornerDecorator = GetTemplateChild("PART_AdornerDecorator") as AdornerDecorator;

            ChangeAdornersCount(0);

            if (_adornerDecorator != null)
            {
                this.SetValue(ContentLayerProperty, _adornerDecorator.Child);
            }
        }


        #region DependencyProperty 'ContentLayer'
        /// <summary>
        /// sets or gets the ContentLayer
        /// </summary>
        public FrameworkElement ContentLayer
        {
            get { return (FrameworkElement)this.GetValue(ContentLayerProperty); }
            //set { this.SetValue(ContentLayerProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ContentLayer
        /// </summary>
        public static readonly DependencyProperty ContentLayerProperty = DependencyProperty.Register(
            "ContentLayer",
            typeof(FrameworkElement),
            typeof(OverlayAdornerDecorator),
            new PropertyMetadata(null, new PropertyChangedCallback(OnContentLayerChangedCallback))
        );

        private static void OnContentLayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OverlayAdornerDecorator obj = d as OverlayAdornerDecorator;
            if (obj != null)
            {
            }
        }

        #endregion
        

        #region IsAdornerLayerVisible

        /// <summary>
        /// IsAdornerLayerVisible Read-Only Dependency Property
        /// </summary>
        private static readonly DependencyPropertyKey IsAdornerLayerVisibleKey
            = DependencyProperty.RegisterReadOnly("IsAdornerLayerVisible", typeof(bool), typeof(OverlayAdornerDecorator),
                new FrameworkPropertyMetadata(false));

        private static readonly DependencyProperty IsAdornerLayerVisibleProperty
            = IsAdornerLayerVisibleKey.DependencyProperty;

        /// <summary>
        /// Gets the IsAdornerLayerVisible property.
        /// </summary>
        public bool IsAdornerLayerVisible
        {
            get { return (bool)GetValue(IsAdornerLayerVisibleProperty); }
        }

        /// <summary>
        /// Provides a secure method for setting the IsAdornerLayerVisible property.  
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        private void SetIsAdornerLayerVisible(bool value)
        {
            SetValue(IsAdornerLayerVisibleKey, value);
        }

        #endregion

        #region Private Helpers

        private void ChangeAdornersCount(int adornersCount)
        {
            _adornersCount = adornersCount;
            SetIsAdornerLayerVisible(_adornersCount > 0);
        }

        #endregion

        #region Public Helpers

        /// <summary>
        /// Adds an adorner to the controlled adorner layer.
        /// </summary>
        /// <param name="adorner">The adorner.</param>
        public void AddAdorner(Adorner adorner)
        {
            if (_adornerDecorator != null)
            {
                _adornerDecorator.AdornerLayer.Add(adorner);
                ChangeAdornersCount(_adornersCount + 1);
            }
        }

        /// <summary>
        /// Removes an adorner from the controlled adorner layer.
        /// </summary>
        /// <param name="adorner">The adorner.</param>
        public void RemoveAdorner(Adorner adorner)
        {
            if (_adornerDecorator != null)
            {
                _adornerDecorator.AdornerLayer.Remove(adorner);
                ChangeAdornersCount(_adornersCount - 1);
            }
        }

        /// <summary>
        /// Finds the nex overlay adorner decorator within the visual tree.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The next overlay adorner decorator</returns>
        public static OverlayAdornerDecorator FindOverlayAdornerDecorator(UIElement element)
        {
            var overlayAdornerDecorator = element as OverlayAdornerDecorator;
            if (overlayAdornerDecorator != null)
            {
                return overlayAdornerDecorator;
            }
            else if (element != null)
            {
                return FindOverlayAdornerDecorator(VisualTreeHelper.GetParent(element) as UIElement);
            }
            return null;
        }

        #endregion
    }

}
