using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UIControls
{
    public class TransitionControl : ContentControl
    {
        ContentPresenter _content;

        public TransitionControl()
        {
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

        }
        
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            // render current view into bitmap
            //RenderTargetBitmap imageCopy = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Default);
            //DrawingVisual dv = new DrawingVisual();
            //using (DrawingContext dc = dv.RenderOpen())
            //{
            //    VisualBrush vb = new VisualBrush(this);
            //    dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), new Size(this.ActualWidth, this.ActualHeight)));
            //}

            //imageCopy.Render(dv);

            base.OnContentChanged(oldContent, newContent);
        }

        protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }


    }
}
