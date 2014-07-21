using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace UIControls
{
    public class BlendTransition : Transition
    {
        private void blend(FrameworkElement foreground, FrameworkElement background)
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

            fadeInStory.Begin(foreground, false);
            fadeOutStory.Begin(background, false);
        }

        public override void ForwardAnimation(FrameworkElement foreground, FrameworkElement background)
        {
            this.blend(foreground, background);
        }

        public override void BackwardAnimation(FrameworkElement foreground, FrameworkElement background)
        {
            this.blend(foreground, background);
        }
    }
}
