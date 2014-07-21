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
        private Transition nextTransition = null;
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

            if (nextTransition != null)
            {
                if (isBackward)
                {
                    nextTransition.BackwardAnimation(_content, _oldContent);
                }
                else
                {
                    nextTransition.ForwardAnimation(_content, _oldContent);
                }
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

            nextTransition = null;
            isBackward = false;

            if (oldContent != null && newContent != null)
            {
                
                nextTransition = Transitions.FirstOrDefault(t => t.From == oldContent.GetType() && t.To == newContent.GetType());
                if (nextTransition == null)
                {
                    nextTransition = Transitions.FirstOrDefault(t => t.To == oldContent.GetType() && t.From == newContent.GetType());
                    if (nextTransition != null)
                    {
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
        
    }

    public abstract class Transition : DependencyObject
    {
        abstract public void ForwardAnimation(FrameworkElement foreground, FrameworkElement background);

        abstract public void BackwardAnimation(FrameworkElement foreground, FrameworkElement background);

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
    }




}
