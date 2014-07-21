using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UIControls
{
    public class SlideInTransition : Transition
    {

        public override void ForwardAnimation(FrameworkElement foreground, FrameworkElement background)
        {
            var slideInStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
            DoubleAnimation da = new DoubleAnimation();
            da.From = Application.Current.MainWindow.ActualWidth;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            Storyboard.SetTargetProperty(da, new PropertyPath("(FrameworkElement.RenderTransform).(TranslateTransform.X)"));
            da.EasingFunction = new PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
            slideInStory.Children.Add(da);

            foreground.RenderTransform = new TranslateTransform();

            slideInStory.Begin(foreground, false);
        }

        public override void BackwardAnimation(FrameworkElement foreground, FrameworkElement background)
        {
            var slideOutStory = new Storyboard() { FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop };
            var da = new DoubleAnimation();
            da.From = 0;
            da.To = Application.Current.MainWindow.ActualWidth;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            Storyboard.SetTargetProperty(da, new PropertyPath("(FrameworkElement.RenderTransform).(TranslateTransform.X)"));
            da.EasingFunction = new PowerEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
            slideOutStory.Children.Add(da);

            background.RenderTransform = new TranslateTransform();

            Panel.SetZIndex(background, 1);
            slideOutStory.Completed += (s, args) =>
            {
                Panel.SetZIndex(background, 0);
            };
            slideOutStory.Begin(background, false);
        }
    }
}
