using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace UIControls
{
    public class DoubleTapHelper : DependencyObject
    {
        static Dictionary<UIElement, DoubleTapinfo> _dict = new Dictionary<UIElement, DoubleTapinfo>();


        #region RoutedEvent 'TouchDoubleTap'
        public class TouchDoubleTapEventArgs : RoutedEventArgs
        {
            public TouchDoubleTapEventArgs(Point p)
            {
                this.RoutedEvent = TouchDoubleTapEvent;
                this.Position = p;
            }
            public Point Position { get; private set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public delegate void TouchDoubleTapEventHandler(object sender, TouchDoubleTapEventArgs e);

        /// <summary>
        /// RoutedEvent TouchDoubleTapEvent
        /// </summary>
        public static readonly RoutedEvent TouchDoubleTapEvent = EventManager.RegisterRoutedEvent(
            "TouchDoubleTap",
            RoutingStrategy.Bubble,
            typeof(TouchDoubleTapEventHandler),
            typeof(DoubleTapHelper)
            );
        #endregion
        

        #region AttachedDependencyProperty 'IsEnabled'
        /// <summary>
        /// sets or gets the Dragable
        /// </summary>
        public static void SetIsEnabled(UIElement element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }
        public static bool GetIsEnabled(UIElement element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// DependencyProperty IsEnabled
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(DoubleTapHelper),
            new PropertyMetadata(false, IsEnabledPropertyChangedCallback)
        );
        private static void IsEnabledPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                if (!_dict.ContainsKey(element))
                {
                    var info = new DoubleTapinfo();
                    _dict[element] = info;
                }
                element.TouchDown += element_PreviewTouchDown;
                element.TouchMove += element_PreviewTouchMove;
            }
            // todo: remove control from dict if property is set to false again!
        }
        
        static void element_PreviewTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                var info = _dict[element];
                if (info.TouchCounter == 1)
                {
                    var dist = e.GetTouchPoint(element).Position - info.Position;
                    if (dist.Length > info.Threshold)
                    {
                        // abort
                        info.Reset();
                    }
                }
            }
        }

        static void element_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                var info = _dict[element];
                if (info.TouchCounter == 0)
                {
                    info.TouchCounter++;
                    info.Position = e.GetTouchPoint(element).Position;
                    info.Timer.Start();
                }
                else if (info.TouchCounter == 1)
                {
                    // check
                    var dist = e.GetTouchPoint(element).Position - info.Position;
                    if (dist.Length > info.Threshold)
                    {
                        info.Reset();
                    }
                    else
                    {
                        // double tap detected
                        element.RaiseEvent(new TouchDoubleTapEventArgs(info.Position));
                        info.Reset();
                        e.Handled = true;
                    }
                }
                else
                {
                    // abort double tap
                    info.Reset();
                }
            }
        }
        
        #endregion
        
    }

    internal class DoubleTapinfo
    {
        public DoubleTapinfo()
        {
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };
            Timer.Tick += Timer_Tick;
            Threshold = 30;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            TouchCounter = 0;
            Timer.Stop();
        }
        
        public int TouchCounter { get; set; }

        public DispatcherTimer Timer { get; set; }

        public Point Position { get; set; }

        public int Threshold { get; set; }
    }
}
