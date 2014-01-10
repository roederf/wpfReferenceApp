using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UIControls.Base;

namespace UIControls.Components
{
    public class ThumbnailSlider: ListBox
    {
        Storyboard _slideStory = new Storyboard();
        DoubleAnimation _anim = new DoubleAnimation();

        double _numVisibleItems = 0.0;

        Grid _content = null;
        ScrollViewer _scroller = null;

        double _currentWidth = 0.0;
        double _currentHeight = 0.0;

        double _scaledWidth = 0.0;
        double _scaledHeight = 0.0;

        double _itemWidth = 0.0;

        int _availableSteps = 0;
        int _currentStep = 0;
        int _numSteps = 10;

        public ThumbnailSlider()
        {
            this.SizeChanged += ThumbnailSliderControl_SizeChanged;
        }

        void ThumbnailSliderControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSizeParameters();
        }

        void UpdateSizeParameters()
        {
            _currentWidth = this.ActualWidth;
            _currentHeight = this.ActualHeight;

            _content.Width = _currentWidth;
            _content.Height = _currentHeight;

            UpdateScaledSize();

            _numVisibleItems = Math.Floor(_scaledWidth / _itemWidth);
            _availableSteps = _numSteps - (int)_numVisibleItems;

            if (_scroller != null)
            {
                _scroller.Margin = new Thickness(0, 0, 0, -Offset);
                _scroller.Height = Offset;
                _scroller.Width = _numVisibleItems * _itemWidth;
                _scroller.ManipulationBoundaryFeedback += _scroller_ManipulationBoundaryFeedback;
            }
        }

        void _scroller_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        void UpdateScaledSize()
        {
            _scaledHeight = _currentHeight - (Offset);
            _scaledWidth = (_currentHeight - (Offset)) / 3 * 4;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _numSteps = Items.Count;

            foreach (var item in Items)
            {
                FrameworkElement fe = ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                fe.Measure(constraint);
                _itemWidth = fe.DesiredSize.Width;
            }

            UpdateSizeParameters();

            return base.MeasureOverride(constraint);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _content = this.GetTemplateChild("PART_Content") as Grid;
            _scroller = this.GetTemplateChild("PART_SCrollViewer") as ScrollViewer;

            if (_scroller != null)
            {
                _scroller.Margin = new Thickness(0, 0, -Offset, 0);
                _scroller.Height = Offset;
                _scroller.ScrollChanged += _scroller_ScrollChanged;
            }
            
            if (_content != null)
            {
                _content.RenderTransform = new ScaleTransform();

                _anim.Duration = TimeSpan.FromMilliseconds(200);
                _anim.EasingFunction = new SineEase() { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
                _slideStory.Children.Add(_anim);
                _slideStory.Completed += _slideStory_Completed;

                Storyboard.SetTarget(_anim, _content);
                Storyboard.SetTargetProperty(_anim, new PropertyPath("(Grid.Height)"));
            }
        }

        void _scroller_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int newstep = (int)(e.HorizontalOffset / _itemWidth);

            _currentStep = newstep;
        }

        void _slideStory_Completed(object sender, EventArgs e)
        {
            if (IsOpen == true)
            {
                this.RaiseEvent(new StateChangedEventArgs() { IsOpen = true });
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                this.RaiseEvent(new StateChangedEventArgs() { IsOpen = false });
            }
        }

        private void doScaleAnimation(bool show)
        {
            if (show)
            {
                _anim.To = _scaledHeight;
                this.Visibility = Visibility.Visible;
            }
            else
            {
                _anim.To = _currentHeight;
            }

            _slideStory.Begin();
        }

        private void SlideTo(int index, bool animated = true)
        {
            if (animated)
            {
                DoubleAnimation anim = new DoubleAnimation();

                anim.To = _currentStep * _itemWidth;
                anim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
                anim.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };

                Storyboard sb = new Storyboard();
                sb.Children.Add(anim);

                Storyboard.SetTarget(anim, this);
                Storyboard.SetTargetProperty(anim, new PropertyPath("HorizontalScrollOffset"));

                sb.Begin(this);
            }
            else
            {
                this.HorizontalScrollOffset = _currentStep * _itemWidth;
            }
        }
                
        #region DependencyProperty 'Preview'
        /// <summary>
        /// sets or gets the Preview
        /// </summary>
        public ImageSource Preview
        {
        get { return (ImageSource)this.GetValue(PreviewProperty); }
        set { this.SetValue(PreviewProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Preview
        /// </summary>
        public static readonly DependencyProperty PreviewProperty = DependencyProperty.Register(
            "Preview",
            typeof(ImageSource),
            typeof(ThumbnailSlider),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'Offset'
        /// <summary>
        /// sets or gets the Offset
        /// </summary>
        public double Offset
        {
            get { return (double)this.GetValue(OffsetProperty); }
            set { this.SetValue(OffsetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Offset
        /// </summary>
        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset",
            typeof(double),
            typeof(ThumbnailSlider),
            new PropertyMetadata(200.0, OffsetPropertyChangedCallback)
        );
        private static void OffsetPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ThumbnailSlider _this = sender as ThumbnailSlider;
            if (_this != null)
            {
                _this.UpdateScaledSize();
            }
        }

        #endregion
        
        #region DependencyProperty 'ViewTemplate'
        /// <summary>
        /// sets or gets the PreviewTemplate
        /// </summary>
        public DataTemplate ViewTemplate
        {
            get { return (DataTemplate)this.GetValue(ViewTemplateProperty); }
            set { this.SetValue(ViewTemplateProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PreviewTemplate
        /// </summary>
        public static readonly DependencyProperty ViewTemplateProperty = DependencyProperty.Register(
            "ViewTemplate",
            typeof(DataTemplate),
            typeof(ThumbnailSlider),
            new PropertyMetadata(null)
        );
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
            typeof(ThumbnailSlider),
            new PropertyMetadata(false, IsOpenPropertyChangedCallback)
        );
        private static void IsOpenPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ThumbnailSlider _this = sender as ThumbnailSlider;
            if (_this != null)
            {
                _this.doScaleAnimation((bool)e.NewValue);
            }
        }

        #endregion

        #region DependencyProperty 'IsOpen'
        /// <summary>
        /// sets or gets the IsOpen
        /// </summary>
        public bool ShowApplyButton
        {
            get { return (bool)this.GetValue(ShowApplyButtonProperty); }
            set { this.SetValue(ShowApplyButtonProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsOpen
        /// </summary>
        public static readonly DependencyProperty ShowApplyButtonProperty = DependencyProperty.Register(
            "ShowApplyButton",
            typeof(bool),
            typeof(ThumbnailSlider),
            new PropertyMetadata(false, null)
        );
        

        #endregion

        #region Command 'SlideLeftCommand', Parameter: object
        private ICommand _SlideLeftCommand;
        public ICommand SlideLeftCommand
        {
            get
            {
                return _SlideLeftCommand ?? (_SlideLeftCommand = new RelayCommand<object>(OnSlideLeftCommand, CanSlideLeft));
            }
        }

        private bool CanSlideLeft(object param)
        {

            return _currentStep > 0 && (_numSteps > _availableSteps);
        }

        private void OnSlideLeftCommand(object param)
        {
            _currentStep--;

            SlideTo(_currentStep);
        }
        #endregion

        #region Command 'SlideRightCommand', Parameter: object
        private ICommand _SlideRightCommand;
        public ICommand SlideRightCommand
        {
            get
            {
                return _SlideRightCommand ?? (_SlideRightCommand = new RelayCommand<object>(OnSlideRightCommand, CanSlideRight));
            }
        }

        private bool CanSlideRight(object param)
        {
            return _currentStep < (_availableSteps) && (_numSteps > _availableSteps);
        }

        private void OnSlideRightCommand(object param)
        {
            _currentStep++;

            SlideTo(_currentStep);
        }
        #endregion

        #region DependencyProperty 'ApplyCommand'
        /// <summary>
        /// sets or gets the ApplyCommand
        /// </summary>
        public ICommand ApplyCommand
        {
        get { return (ICommand)this.GetValue(ApplyCommandProperty); }
        set { this.SetValue(ApplyCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ApplyCommand
        /// </summary>
        public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register(
            "ApplyCommand",
            typeof(ICommand),
            typeof(ThumbnailSlider),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'CancelCommand'
        /// <summary>
        /// sets or gets the CancelCommand
        /// </summary>
        public ICommand CancelCommand
        {
        get { return (ICommand)this.GetValue(CancelCommandProperty); }
        set { this.SetValue(CancelCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CancelCommand
        /// </summary>
        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            "CancelCommand",
            typeof(ICommand),
            typeof(ThumbnailSlider),
            new PropertyMetadata(null)
        );
        #endregion

        #region RoutedEvent 'StateChangedEvent'
        public class StateChangedEventArgs : RoutedEventArgs
        {
            public StateChangedEventArgs()
            {
                this.RoutedEvent = StateChangedEvent;
            }

            public bool IsOpen { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs e);

        /// <summary>
        /// RoutedEvent OnOpenEvent
        /// </summary>
        public static readonly RoutedEvent StateChangedEvent = EventManager.RegisterRoutedEvent(
            "StateChangedEvent",
            RoutingStrategy.Bubble,
            typeof(StateChangedEventHandler),
            typeof(ThumbnailSlider)
            );
        #endregion

        #region DependencyProperty 'HorizontalScrollOffset'
        /// <summary>
        /// sets or gets the HorizontalScrollOffset
        /// </summary>
        public double HorizontalScrollOffset
        {
            get { return (double)this.GetValue(HorizontalScrollOffsetProperty); }
            set { this.SetValue(HorizontalScrollOffsetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty HorizontalScrollOffset
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollOffsetProperty = DependencyProperty.Register(
            "HorizontalScrollOffset",
            typeof(double),
            typeof(ThumbnailSlider),
            new PropertyMetadata(0.0, HorizontalScrollOffsetPropertyChangedCallback)
        );
        private static void HorizontalScrollOffsetPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ThumbnailSlider _this = sender as ThumbnailSlider;
            if (_this != null)
            {
                _this._scroller.ScrollToHorizontalOffset(_this.HorizontalScrollOffset);
            }
        }
        #endregion
    }
}
