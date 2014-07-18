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
        private TransitionType nextTransition = TransitionType.None;
        private bool isBackward = false;
        FrameworkElement _oldContent, _content;

        public TransitionControl()
        {
            Transitions = new List<Transition>();

            this.AddHandler(TransitionContainerLoadedEvent, new TransitionContainerLoadedEventHandler(OnTransitionContainerLoaded));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _oldContent = GetTemplateChild("PART_OldContent") as FrameworkElement;
            _content = GetTemplateChild("PART_Content") as FrameworkElement;

        }

        void OnTransitionContainerLoaded(object sender, TransitionContainerLoadedEventArgs e)
        {
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (nextTransition == TransitionType.Blend)
            {
                var fadeInStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
                DoubleAnimation fadeIn = new DoubleAnimation();
                fadeIn.From = 0;
                fadeIn.To = 1;
                fadeIn.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut };
                fadeIn.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                fadeInStory.Children.Add(fadeIn);
                Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));

                var fadeOutStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
                DoubleAnimation fadeOut = new DoubleAnimation();
                fadeOut.From = 1;
                fadeOut.To = 0;
                fadeOut.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut };
                fadeOut.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                fadeOutStory.Children.Add(fadeOut);
                Storyboard.SetTargetProperty(fadeOut, new PropertyPath("Opacity"));

                fadeInStory.Begin(_content, false);
                fadeOutStory.Begin(_oldContent, false);
                

            }
            else if (nextTransition == TransitionType.SlideInFromLeft)
            {
                var slideInStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
                DoubleAnimation da = new DoubleAnimation();
                da.From = Application.Current.MainWindow.ActualWidth;
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                Storyboard.SetTargetProperty(da, new PropertyPath("(FrameworkElement.RenderTransform).(TranslateTransform.X)"));
                da.EasingFunction = new PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
                slideInStory.Children.Add(da);

                _content.RenderTransform = new TranslateTransform();

                var slideOutStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
                da = new DoubleAnimation();
                da.From = 0;
                da.To = Application.Current.MainWindow.ActualWidth;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                Storyboard.SetTargetProperty(da, new PropertyPath("(FrameworkElement.RenderTransform).(TranslateTransform.X)"));
                da.EasingFunction = new PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
                slideOutStory.Children.Add(da);

                _oldContent.RenderTransform = new TranslateTransform();

                if (isBackward)
                {
                    Panel.SetZIndex(_oldContent, 1);
                    slideOutStory.Completed += (s, args) =>
                        {
                            Panel.SetZIndex(_oldContent, 0);
                        };
                    slideOutStory.Begin(_oldContent, false);

                }
                else
                {
                    slideInStory.Begin(_content, false);
                }
                //slideOutStory.Begin(_oldContent, false);
            }

            e.Handled = true;
        }
        
        private BitmapSource renderCurrentVisual()
        {
            if (this.ActualWidth > 0 && this.ActualHeight > 0)
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

                return rtb;
            }
            else
            {
                return null;
            }
        }
                
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            // render current view into bitmap
            OldContentImage = renderCurrentVisual();

            nextTransition = TransitionType.None;
            isBackward = false;

            if (oldContent != null && newContent != null)
            {
                
                var forward = Transitions.FirstOrDefault(t => t.From == oldContent.GetType() && t.To == newContent.GetType());
                if (forward != null)
                {
                    nextTransition = forward.Type;
                }
                else
                {
                    var backward = Transitions.FirstOrDefault(t => t.To == oldContent.GetType() && t.From == newContent.GetType());
                    if (backward != null)
                    {
                        nextTransition = backward.Type;
                        isBackward = true;
                    }
                }
            }

            base.OnContentChanged(oldContent, newContent);
        }
        
        #region AttachedDependencyProperty 'EnableTransitions'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetEnableTransitions(UIElement element, bool value)
        {
            element.SetValue(EnableTransitionsProperty, value);
        }
        public static bool GetEnableTransitions(UIElement element)
        {
            return (bool)element.GetValue(EnableTransitionsProperty);
        }

        /// <summary>
        /// DependencyProperty EnableTransitions
        /// </summary>
        public static readonly DependencyProperty EnableTransitionsProperty = DependencyProperty.RegisterAttached(
            "EnableTransitions",
            typeof(bool),
            typeof(TransitionControl),
            new PropertyMetadata(false, EnableTransitionsPropertyChangedCallback)
        );
        private static void EnableTransitionsPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                FrameworkElement element = sender as FrameworkElement;
                if (element != null)
                {
                    element.Loaded += element_Loaded;
                }

            }
        }

        static void element_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            element.RaiseEvent(new TransitionContainerLoadedEventArgs());
        }

        #endregion
        
        #region RoutedEvent 'TransitionContainerLoaded'
        public class TransitionContainerLoadedEventArgs : RoutedEventArgs
        {
            public TransitionContainerLoadedEventArgs()
            {
                this.RoutedEvent = TransitionContainerLoadedEvent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public delegate void TransitionContainerLoadedEventHandler(object sender, TransitionContainerLoadedEventArgs e);

        /// <summary>
        /// RoutedEvent TransitionContainerLoadedEvent
        /// </summary>
        public static readonly RoutedEvent TransitionContainerLoadedEvent = EventManager.RegisterRoutedEvent(
            "TransitionContainerLoaded",
            RoutingStrategy.Bubble,
            typeof(TransitionContainerLoadedEventHandler),
            typeof(TransitionControl)
            );
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
        
        #region DependencyProperty 'Transitions'
        /// <summary>
        /// sets or gets the Transitions
        /// </summary>
        public List<Transition> Transitions
        {
        get { return (List<Transition>)this.GetValue(TransitionsProperty); }
        set { this.SetValue(TransitionsProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Transitions
        /// </summary>
        public static readonly DependencyProperty TransitionsProperty = DependencyProperty.Register(
            "Transitions",
            typeof(List<Transition>),
            typeof(TransitionControl),
            new PropertyMetadata(null)
        );
        #endregion
        

        public enum TransitionType
        {
            None,
            Blend,
            SlideInFromLeft
        }
    }

    public class Transition : Control
    {

        #region DependencyProperty 'From'
        /// <summary>
        /// sets or gets the From
        /// </summary>
        public Type From
        {
        get { return (Type)this.GetValue(FromProperty); }
        set { this.SetValue(FromProperty, value); }
        }
        /// <summary>
        /// DependencyProperty From
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From",
            typeof(Type),
            typeof(Transition),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'To'
        /// <summary>
        /// sets or gets the To
        /// </summary>
        public Type To
        {
        get { return (Type)this.GetValue(ToProperty); }
        set { this.SetValue(ToProperty, value); }
        }
        /// <summary>
        /// DependencyProperty To
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To",
            typeof(Type),
            typeof(Transition),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'Type'
        /// <summary>
        /// sets or gets the Type
        /// </summary>
        public TransitionControl.TransitionType Type
        {
        get { return (TransitionControl.TransitionType)this.GetValue(TypeProperty); }
        set { this.SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Type
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type",
            typeof(TransitionControl.TransitionType),
            typeof(Transition),
            new PropertyMetadata(TransitionControl.TransitionType.None)
        );
        #endregion
        
    }
}
