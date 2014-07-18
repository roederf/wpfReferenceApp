using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace UIControls
{
    public class TransitionControl : ContentControl
    {
        public TransitionControl()
        {
            this.Loaded += TransitionControl_Loaded;
        }

        void TransitionControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
                
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            // render current view into bitmap
            if (this.ActualWidth > 0 && this.ActualHeight > 0 && oldContent != null)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(this);
                    vb.Stretch = Stretch.None;
                    dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), new Size(this.ActualWidth, this.ActualHeight)));
                }

                rtb.Render(dv);

                OldContentImage = rtb;
            }
            else
            {
                OldContentImage = null;
            }

            base.OnContentChanged(oldContent, newContent);
        }
        
        #region AttachedDependencyProperty 'Transition'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetTransition(UIElement element, TransitionType value)
        {
            element.SetValue(TransitionProperty, value);
        }
        public static TransitionType GetTransition(UIElement element)
        {
            return (TransitionType)element.GetValue(TransitionProperty);
        }

        /// <summary>
        /// DependencyProperty Transition
        /// </summary>
        public static readonly DependencyProperty TransitionProperty = DependencyProperty.RegisterAttached(
            "Transition",
            typeof(TransitionType),
            typeof(TransitionControl),
            new PropertyMetadata(TransitionType.None, TransitionPropertyChangedCallback)
        );
        private static void TransitionPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                element.Loaded += element_Loaded;
            }
        }

        static void element_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                if (TransitionControl.GetTransition(element) == TransitionType.Blend)
                {
                    var story = element.TryFindResource("blendStoryboard") as Storyboard;
                    if (story != null)
                    {
                        story.Begin(element, false);
                    }
                }
            }
        }
        #endregion
        
        #region DependencyProperty 'OldContentImage'
        /// <summary>
        /// sets or gets the OldContentImage
        /// </summary>
        public BitmapSource OldContentImage
        {
            get { return (BitmapSource)this.GetValue(OldContentImageProperty); }
            set { this.SetValue(OldContentImageProperty, value); }
        }
        /// <summary>
        /// DependencyProperty OldContentImage
        /// </summary>
        public static readonly DependencyProperty OldContentImageProperty = DependencyProperty.Register(
            "OldContentImage",
            typeof(BitmapSource),
            typeof(TransitionControl),
            new PropertyMetadata(null, OldContentImagePropertyChangedCallback)
        );
        private static void OldContentImagePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TransitionControl _this = sender as TransitionControl;
            if (_this != null)
            {

            }
        }

        #endregion
        

        public enum TransitionType
        {
            None,
            Blend,
            Slide
        }
    }
}
