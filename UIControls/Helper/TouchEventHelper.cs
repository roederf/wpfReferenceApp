using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace UIControls.Helper
{
    public class TouchEventHelper : DependencyObject
    {
        private Queue<TouchEventHelper.TouchEventArgs> queue = new Queue<TouchEventHelper.TouchEventArgs>();
        private DispatcherTimer timer = new DispatcherTimer();

        public TouchEventHelper(Control c, bool ignoreManipulation)
        {
            c.TouchDown += c_TouchDown;
            c.TouchMove += c_TouchMove;
            c.TouchUp += c_TouchUp;

            if (ignoreManipulation)
            {
                c.ManipulationStarting += c_ManipulationStarting;
                c.ManipulationStarted += c_ManipulationStarted;
                c.ManipulationInertiaStarting += c_ManipulationInertiaStarting;
                c.ManipulationDelta += c_ManipulationDelta;
                c.ManipulationCompleted += c_ManipulationCompleted;
                c.ManipulationBoundaryFeedback += c_ManipulationBoundaryFeedback;
            }

            timer.Interval = TimeSpan.FromMilliseconds(1000.0/30.0);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void c_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        void c_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            e.Handled = true;
        }

        void c_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            e.Handled = true;
        }

        void c_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.Handled = true;
        }

        void c_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            e.Handled = true;
        }

        void c_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.Handled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (queue.Count > 0)
            {
                var nextEvent = queue.Dequeue();

                if (nextEvent.Event == UIElement.TouchDownEvent)
                {
                    if (TouchDown != null)
                    {
                        TouchDown(this, nextEvent);
                    }
                }
                else if (nextEvent.Event == UIElement.TouchUpEvent)
                {
                    if (TouchUp != null)
                    {
                        TouchUp(this, nextEvent);
                    }
                }
                else if (nextEvent.Event == UIElement.TouchMoveEvent)
                {
                    while (queue.Count > 0 && queue.Peek().Event == UIElement.TouchMoveEvent)
                    {
                        nextEvent = queue.Dequeue();
                    }

                    if (TouchMove != null)
                    {
                        TouchMove(this, nextEvent);
                    }
                }
            }
        }
        
        void c_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            
            queue.Enqueue(new TouchEventArgs()
            {
                Timestamp = e.Timestamp,
                TouchDevice = e.TouchDevice,
                OriginalSource = e.OriginalSource,
                Source = e.Source,
                Event = e.RoutedEvent
            });

            e.Handled = true;
        }

        void c_TouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            queue.Enqueue(new TouchEventArgs()
            {
                Timestamp = e.Timestamp,
                TouchDevice = e.TouchDevice,
                OriginalSource = e.OriginalSource,
                Source = e.Source,
                Event = e.RoutedEvent
            });

            e.Handled = true;
        }

        void c_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            queue.Enqueue(new TouchEventArgs()
            {
                Timestamp = e.Timestamp,
                TouchDevice = e.TouchDevice,
                OriginalSource = e.OriginalSource,
                Source = e.Source,
                Event = e.RoutedEvent
            });

            e.Handled = true;
        }

        public class TouchEventArgs : EventArgs
        {
            public int Timestamp { get; set; }
            public TouchDevice TouchDevice { get; set; }
            public object OriginalSource { get; set; }
            public object Source { get; set; }
            public RoutedEvent Event { get; set; }
        }
                
        public event TouchEventHandler TouchDown;

        public event TouchEventHandler TouchMove;
        
        public event TouchEventHandler TouchUp;

        public delegate void TouchEventHandler(object sender, TouchEventArgs args);
    }

}
