using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace UIControls.Controls
{
    /// <summary>
    /// A control similar to a popup, but based on the adornerlayer
    /// </summary>
    public class AdornerPopup : ContentControl
    {
        #region private members

        private PopupContentAdorner _adorner = null;
        private bool _playAnimationOnSizeChange = false;
        bool _shouldClosePopup = true;
        OverlayAdornerDecorator _overlay = null;

        MouseButtonEventHandler _overlayPreviewClickHandler = null;
        MouseButtonEventHandler _contentPreviewClickHandler = null;

        #endregion

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (newContent != null)
            {
                if (IsOpen)
                    Show();
            }
        }

        #region Properties

        #region DependencyProperty 'PlacementTarget'
        /// <summary>
        /// sets or gets the PlacementTarget
        /// </summary>
        public FrameworkElement PlacementTarget
        {
            get { return (FrameworkElement)this.GetValue(PlacementTargetProperty); }
            set { this.SetValue(PlacementTargetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PlacementTarget
        /// </summary>
        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
            "PlacementTarget",
            typeof(FrameworkElement),
            typeof(AdornerPopup),
            new PropertyMetadata(null)
        );

        #endregion

        #region DependencyProperty 'Placement'
        /// <summary>
        /// sets or gets the Placement
        /// </summary>
        public PlacementMode Placement
        {
            get { return (PlacementMode)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Placement
        /// </summary>
        public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register(
            "Placement",
            typeof(PlacementMode),
            typeof(AdornerPopup),
            new PropertyMetadata(PlacementMode.Bottom, PlacementPropertyChangedCallback)
        );
        private static void PlacementPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //AdornerPopup ap = sender as AdornerPopup;
            //if (ap != null && ap._overlayBorder!=null)
            //{
            //    if ((PlacementMode)e.NewValue == PlacementMode.Top)
            //    {
            //        ap._overlayBorder.VerticalAlignment = VerticalAlignment.Bottom;
            //        ap._overlayBorder.Margin = new Thickness(ap.Padding.Left, ap.Padding.Top, ap.Padding.Right, ap.Padding.Bottom);
            //    }
            //    else
            //    {
            //        ap._overlayBorder.VerticalAlignment = VerticalAlignment.Top;
            //        ap._overlayBorder.Margin = new Thickness(ap.Padding.Left, ap.Padding.Top - 1, ap.Padding.Right, ap.Padding.Bottom);
            //    }
            //}
        }

        #endregion

        #region DependencyProperty 'IsOpen'
        /// <summary>
        /// sets or gets the IsOpen
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)this.GetValue(IsOpenProperty); }
            set { this.SetValue(IsOpenProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsOpen
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen",
            typeof(bool),
            typeof(AdornerPopup),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsOpenChangedCallback))
        );

        private static void OnIsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdornerPopup obj = d as AdornerPopup;
            if (obj != null)
            {
                
                if ((bool)e.NewValue == true)
                {
                    if(obj.Content != null)
                    {
                        obj.Show();
                    }
                }
                else
                {
                    obj.Hide();
                }
                
            }
        }

        #endregion
        
        #region DependencyProperty 'HorizontalOffset'
        /// <summary>
        /// sets or gets the HorizontalOffset
        /// </summary>
        public double HorizontalOffset
        {
            get { return (double)this.GetValue(HorizontalOffsetProperty); }
            set { this.SetValue(HorizontalOffsetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty HorizontalOffset
        /// </summary>
        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register(
            "HorizontalOffset",
            typeof(double),
            typeof(AdornerPopup),
            new PropertyMetadata(0.0)
        );

        #endregion
        
        #region DependencyProperty 'VerticalOffset'
        /// <summary>
        /// sets or gets the VerticalOffset
        /// </summary>
        public double VerticalOffset
        {
            get { return (double)this.GetValue(VerticalOffsetProperty); }
            set { this.SetValue(VerticalOffsetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty VerticalOffset
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register(
            "VerticalOffset",
            typeof(double),
            typeof(AdornerPopup),
            new PropertyMetadata(0.0)
        );

        #endregion
        
        #region DependencyProperty 'IgnoreLayerBounds'
        /// <summary>
        /// sets or gets the IgnoreLayerBounds
        /// </summary>
        public bool IgnoreLayerBounds
        {
            get { return (bool)this.GetValue(IgnoreLayerBoundsProperty); }
            set { this.SetValue(IgnoreLayerBoundsProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IgnoreLayerBounds
        /// </summary>
        public static readonly DependencyProperty IgnoreLayerBoundsProperty = DependencyProperty.Register(
            "IgnoreLayerBounds",
            typeof(bool),
            typeof(AdornerPopup),
            new PropertyMetadata(false)
        );
        #endregion
        
        #region DependencyProperty 'UseOpenAnimation'
        /// <summary>
        /// sets or gets the UseOpenAnimation
        /// </summary>
        public bool UseOpenAnimation
        {
            get { return (bool)this.GetValue(UseOpenAnimationProperty); }
            set { this.SetValue(UseOpenAnimationProperty, value); }
        }
        /// <summary>
        /// DependencyProperty UseOpenAnimation
        /// </summary>
        public static readonly DependencyProperty UseOpenAnimationProperty = DependencyProperty.Register(
            "UseOpenAnimation",
            typeof(bool),
            typeof(AdornerPopup),
            new PropertyMetadata(true)
        );

        #endregion


        #region DependencyProperty 'AutoClose'
        /// <summary>
        /// sets or gets the AutoClose
        /// </summary>
        public bool AutoClose
        {
        get { return (bool)this.GetValue(AutoCloseProperty); }
        set { this.SetValue(AutoCloseProperty, value); }
        }
        /// <summary>
        /// DependencyProperty AutoClose
        /// </summary>
        public static readonly DependencyProperty AutoCloseProperty = DependencyProperty.Register(
            "AutoClose",
            typeof(bool),
            typeof(AdornerPopup),
            new PropertyMetadata(false)
        );
        #endregion
        
        
        #endregion

        #region Commands

        static public readonly RoutedCommand CloseCommand = new RoutedCommand();

        static public readonly RoutedCommand WantCloseCommand = new RoutedCommand();

        #endregion

        #region internal methods

        void tryToFocusAChild(FrameworkElement element)
        {
            while (element.Focus() == false && element != null)
            {
                if (VisualTreeHelper.GetChildrenCount(element) > 0)
                    element = VisualTreeHelper.GetChild(element, 0) as FrameworkElement;
                else
                    break;
            }
        }

        private void UpdatePlacement(Size desiredSize)
        {
            Size targetSize = new Size(PlacementTarget.ActualWidth, PlacementTarget.ActualHeight);
            OverlayAdornerDecorator overlayDecorator = OverlayAdornerDecorator.FindOverlayAdornerDecorator(PlacementTarget);
            if (overlayDecorator != null)
            {
                Size layerSize = new Size(overlayDecorator.ActualWidth, overlayDecorator.ActualHeight);
                Point targetTopLeft = PlacementTarget.TranslatePoint(new Point(0, 0), overlayDecorator);

                // adjust left and top offset (only 'Bottom' and 'Top' are used at the moment!)
                switch (Placement)
                {
                    case PlacementMode.Bottom:
                        _adorner.TopOffset = targetSize.Height + VerticalOffset;
                        _adorner.LeftOffset = 0.0 + HorizontalOffset;
                        break;
                    case PlacementMode.Center:
                        _adorner.TopOffset = targetSize.Height + VerticalOffset;
                        _adorner.LeftOffset = targetSize.Width * 0.5 - desiredSize.Width * 0.5 * HorizontalOffset;
                        break;
                    case PlacementMode.Top:
                        _adorner.TopOffset = VerticalOffset - desiredSize.Height;
                        _adorner.LeftOffset = 0.0 + HorizontalOffset;
                        break;
                    case PlacementMode.Left:
                        _adorner.TopOffset = 0.0 + VerticalOffset;
                        _adorner.LeftOffset = HorizontalOffset - desiredSize.Width;
                        break;
                    case PlacementMode.Right:
                        _adorner.LeftOffset = HorizontalOffset + targetSize.Width;
                        _adorner.TopOffset = VerticalOffset;
                        break;
                    case PlacementMode.Relative:
                        _adorner.TopOffset = VerticalOffset;
                        _adorner.LeftOffset = HorizontalOffset;
                        break;
                    default:
                        _adorner.TopOffset = VerticalOffset;
                        _adorner.LeftOffset = HorizontalOffset;
                        break;
                }
                if (!IgnoreLayerBounds)
                {
                    // check bounds
                    if (targetTopLeft.X + desiredSize.Width + HorizontalOffset > layerSize.Width)
                    {
                        double shiftX = targetTopLeft.X + desiredSize.Width + HorizontalOffset - layerSize.Width;
                        _adorner.LeftOffset -= shiftX;
                    }
                    if (targetTopLeft.Y + _adorner.TopOffset + desiredSize.Height > layerSize.Height)
                    {
                        double shiftY = targetTopLeft.Y + desiredSize.Height - layerSize.Height;
                        _adorner.TopOffset -= shiftY;
                    }

                    if (targetTopLeft.X + _adorner.LeftOffset < 0)
                        _adorner.LeftOffset = -targetTopLeft.X + HorizontalOffset;
                    if (targetTopLeft.Y + _adorner.TopOffset < 0)
                        _adorner.TopOffset = -targetTopLeft.Y + VerticalOffset;
                }
            }
        }


        void animate(FrameworkElement fm, double ms)
        {
            TranslateTransform trans = new TranslateTransform();
            trans.X = 0;
            double maxHeight = 0;
            if (PlacementTarget != null)
            {
                OverlayAdornerDecorator overlayDecorator = OverlayAdornerDecorator.FindOverlayAdornerDecorator(PlacementTarget);
                if (overlayDecorator!= null)
                    maxHeight = overlayDecorator.ActualHeight;
            }
            if (!Double.IsNaN(MaxHeight) && !Double.IsInfinity(MaxHeight))
            {
                maxHeight = MaxHeight;
            }
            
            if (Placement == PlacementMode.Top)
            {
                trans.Y = maxHeight;
            }
            else
            {
                trans.Y = -maxHeight;
            }

            DoubleAnimation openAnimation = new DoubleAnimation();
            openAnimation.Duration = TimeSpan.FromMilliseconds(ms * 0.5);
            
            openAnimation.To = 0;
            openAnimation.DecelerationRatio = 0.3;
            openAnimation.FillBehavior = FillBehavior.HoldEnd;
            // need to update because the glass Control needs the background before we start the animation

            fm.UpdateLayout();
            fm.RenderTransform = trans;
            this.UpdatePlacement(fm.DesiredSize);

            trans.BeginAnimation(TranslateTransform.YProperty, openAnimation);
        }

        void fmContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePlacement(e.NewSize);
            if (_playAnimationOnSizeChange)
            {
                _playAnimationOnSizeChange = false;
                FrameworkElement fm = sender as FrameworkElement;
                if (UseOpenAnimation && fm != null)
                {
                    animate(fm, 600);
                }
            }
        }

        #endregion

        public void Show()
        {
            if (PlacementTarget != null)
            {
                FrameworkElement fmContent = Content as FrameworkElement;
                Grid outerGrid = new Grid();

                if (_adorner == null)
                {
                    // to remove the content as a logical child of 'this'
                    this.Content = null;

                    if (fmContent != null)
                    {
                        outerGrid.ClipToBounds = UseOpenAnimation;
                        outerGrid.Children.Add(fmContent);
                    }

                    _adorner = new PopupContentAdorner(PlacementTarget, outerGrid, this);

                    _adorner.DataContext = PlacementTarget.DataContext;
                }

                //_adorner.UpdateLayout();
                if (fmContent != null)
                {
                    UpdatePlacement(new Size(fmContent.ActualWidth, fmContent.ActualHeight));

                    fmContent.SizeChanged += new SizeChangedEventHandler(fmContent_SizeChanged);
                }

                _overlay = _adorner.Attach();

                if (AutoClose && _overlay != null)
                {
                    _shouldClosePopup = true;
                    if (_overlayPreviewClickHandler == null)
                    {
                        _overlayPreviewClickHandler = new MouseButtonEventHandler(overlay_PreviewMouseLeftButtonDown);
                        _overlay.PreviewMouseLeftButtonDown += _overlayPreviewClickHandler;
                        _overlay.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(overlay_MouseLeftButtonDown), true);
                    }
                    if (_contentPreviewClickHandler == null)
                    {
                        _contentPreviewClickHandler = new MouseButtonEventHandler(fmContent_PreviewMouseLeftButtonDown);
                        fmContent.PreviewMouseLeftButtonDown += _contentPreviewClickHandler;
                    }
                }

                if (UseOpenAnimation && fmContent != null)
                {
                    if (fmContent.ActualWidth == 0 && fmContent.ActualHeight == 0)
                        _playAnimationOnSizeChange = true;
                    else
                    {
                        animate(fmContent, 600);
                    }
                }

                //tryToFocusAChild(this);
            }
        }

        void overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_shouldClosePopup)
            {
                //Hide();
                IsOpen = false;
            }
        }

        void overlay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _shouldClosePopup = true;
        }

        void fmContent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _shouldClosePopup = false;
        }
        
        public void Hide()
        {
            if (_adorner != null)
            {
                _adorner.Detach();

                if (AutoClose)
                {
                    //if (_adorner.Content != null)
                    //    _adorner.Content.PreviewMouseLeftButtonDown -= _contentPreviewClickHandler;

                    //_overlay.PreviewMouseLeftButtonDown -= _overlayPreviewClickHandler;

                    //_shouldClosePopup = true;
                }

                object tmp = _adorner.Content;
                
                this.Content = tmp;

                _adorner.RemoveChildren();

                _adorner = null;
            }
        }

        #region RoutedEvent 'PopupCloseClicked'
        public class PopupCloseClickedEventArgs : RoutedEventArgs
        {
            public PopupCloseClickedEventArgs(object param)
            {
                this.Parameter = param;
                this.RoutedEvent = PopupCloseClickedEvent;
            }
            public object Parameter { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public delegate void PopupCloseClickedEventHandler(object sender, PopupCloseClickedEventArgs e);

        /// <summary>
        /// RoutedEvent PopupClosedClickedEvent
        /// </summary>
        public static readonly RoutedEvent PopupCloseClickedEvent = EventManager.RegisterRoutedEvent(
            "PopupCloseClicked",
            RoutingStrategy.Bubble,
            typeof(PopupCloseClickedEventHandler),
            typeof(AdornerPopup)
            );
        #endregion
    }

    class PopupContentAdorner : Adorner
    {

        #region Property 'Content'

        /// <summary>
        /// sets or gets the Content
        /// </summary>
        public UIElement Content { get; set; }

        #endregion

        private readonly VisualCollection _children;

        private readonly AdornerPopup _root;

        public PopupContentAdorner(UIElement adornedElement, FrameworkElement child, AdornerPopup popup)
            : base(adornedElement)
        {
            _children = new VisualCollection(this);

            _root = popup;

            Content = child;

            _children.Add(Content);

            this.CommandBindings.Add(new CommandBinding(AdornerPopup.CloseCommand, Close_Executed, Close_CanExecute));
            this.CommandBindings.Add(new CommandBinding(AdornerPopup.WantCloseCommand, WantClose_Executed, WantClose_CanExecute));
        }

        void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _root.IsOpen = false;
        }

        void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void WantClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _root.RaiseEvent(new AdornerPopup.PopupCloseClickedEventArgs(e.Parameter));
        }

        void WantClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Attaches this instance.
        /// </summary>
        public OverlayAdornerDecorator Attach()
        {
            OverlayAdornerDecorator overlayDecorator = OverlayAdornerDecorator.FindOverlayAdornerDecorator(AdornedElement);

            if (overlayDecorator != null)
            {
                overlayDecorator.AddAdorner(this);
                return overlayDecorator;
            }

            return null;

        }
        
        /// <summary>
        /// Detaches this instance.
        /// </summary>
        public void Detach()
        {

            OverlayAdornerDecorator overlayDecorator = OverlayAdornerDecorator.FindOverlayAdornerDecorator(AdornedElement);

            if (overlayDecorator != null)
            {
                overlayDecorator.RemoveAdorner(this);
            }

        }

        public void RemoveChildren()
        {
            _children.Clear();
        }

        #region override Measure and Offset

        protected override Size MeasureOverride(Size constraint)
        {

            OverlayAdornerDecorator overlayDecorator = OverlayAdornerDecorator.FindOverlayAdornerDecorator(AdornedElement);

            Content.Measure(constraint);

            return Content.DesiredSize;

        }

        protected override Size ArrangeOverride(Size finalSize)
        {

            var adornedElementRect = new Rect(Content.DesiredSize);

            Content.Arrange(adornedElementRect);

            return finalSize;

        }


        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var result = new GeneralTransformGroup();

            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
            return result;
        }

        private double _leftOffset;
        
        public double LeftOffset
        {
            get { return _leftOffset; }
            set
            {
                _leftOffset = value;
                UpdatePosition();
            }
        }

        private double _topOffset;
       
        public double TopOffset
        {
            get { return _topOffset; }
            set
            {
                _topOffset = value;
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            var adorner = (AdornerLayer)Parent;
            if (adorner != null)
            {
                adorner.Update(AdornedElement);
            }
        }

        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _children[index];
        }

        #endregion
    }
}
