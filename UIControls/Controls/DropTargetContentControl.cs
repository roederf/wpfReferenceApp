using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UIControls.Helper;

namespace UIControls.Controls
{
    public class DropTargetContentControl : ContentControl
    {
        public DropTargetContentControl()
        {
            this.Loaded += DropTargetContentControl_Loaded;
        }

        void DropTargetContentControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window != null)
            {
                window.AddHandler(DragDropHelper.AdornerOpenEvent, new DragDropHelper.AdornerOpenEventHandler(OnAdornerOpen));
                window.AddHandler(DragDropHelper.AdornerClosedEvent, new DragDropHelper.AdornerClosedEventHandler(OnAdornerClosed));
            }
        }

        void OnAdornerOpen(object sender, DragDropHelper.AdornerOpenEventArgs e)
        {
            if (e.Data != null)
            {
                IsDragging = true;
            }
        }

        void OnAdornerClosed(object sender, DragDropHelper.AdornerClosedEventArgs e)
        {
            IsDragging = false;
        }

        #region DependencyProperty 'IsDragging'
        /// <summary>
        /// sets or gets the IsDragging
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)this.GetValue(IsDraggingProperty); }
            set { this.SetValue(IsDraggingProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsDragging
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.Register(
            "IsDragging",
            typeof(bool),
            typeof(DropTargetContentControl),
            new PropertyMetadata(false)
        );
        #endregion

    }
}
