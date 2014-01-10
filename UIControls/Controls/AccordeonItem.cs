using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace UIControls.Controls
{
    public class AccordeonItem: HeaderedContentControl
    {
        public Border _header;
 
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _header = this.GetTemplateChild("PART_Header") as Border;
            if (_header != null)
            {
                _header.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(_header_MouseLeftButtonDown);
            }
        }

        void _header_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Content != null && ItemPosition != AccordeonItemPosition.Single)
            {
                HeaderClicked(this, new HeaderClickedEventArgs());
            }
        }

        #region RoutedEvent 'HeaderClicked'
        public class HeaderClickedEventArgs : RoutedEventArgs
        {
            public HeaderClickedEventArgs()
            {
                this.RoutedEvent = HeaderClickedEvent;
            }
        }

        public event HeaderClickedEventHandler HeaderClicked;

        /// <summary>
        /// 
        /// </summary>
        public delegate void HeaderClickedEventHandler(object sender, HeaderClickedEventArgs e);

        /// <summary>
        /// RoutedEvent HeaderClickedEvent
        /// </summary>
        public static readonly RoutedEvent HeaderClickedEvent = EventManager.RegisterRoutedEvent(
            "HeaderClicked",
            RoutingStrategy.Bubble,
            typeof(HeaderClickedEventHandler),
            typeof(AccordeonItem)
            );
        #endregion
        
        #region DependencyProperty 'IsSelected'
        /// <summary>
        /// sets or gets the IsSelected
        /// </summary>
        public bool IsSelected
        {
        get { return (bool)this.GetValue(IsSelectedProperty); }
        set { this.SetValue(IsSelectedProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsSelected
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion
       
        #region DependencyProperty 'ItemPosition'
        /// <summary>
        /// sets or gets the ItemPosition
        /// </summary>
        public AccordeonItemPosition ItemPosition
        {
            get { return (AccordeonItemPosition)this.GetValue(ItemPositionProperty); }
            set { this.SetValue(ItemPositionProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ItemPosition
        /// </summary>
        public static readonly DependencyProperty ItemPositionProperty = DependencyProperty.Register(
            "ItemPosition",
            typeof(AccordeonItemPosition),
            typeof(AccordeonItem),
            new PropertyMetadata(AccordeonItemPosition.Center)
        );
        #endregion
        
        #region DependencyProperty 'IsLocked'
        /// <summary>
        /// sets or gets the Locked
        /// </summary>
        public bool IsLocked
        {
        get { return (bool)this.GetValue(IsLockedProperty); }
        set { this.SetValue(IsLockedProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Locked
        /// </summary>
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
            "IsLocked",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion
        
        #region DependencyProperty 'ShowLock'
        /// <summary>
        /// sets or gets the ShowLock
        /// </summary>
        public bool ShowLock
        {
        get { return (bool)this.GetValue(ShowLockProperty); }
        set { this.SetValue(ShowLockProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowLock
        /// </summary>
        public static readonly DependencyProperty ShowLockProperty = DependencyProperty.Register(
            "ShowLock",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion
                
        #region DependencyProperty 'ShowEnableToggle'
        /// <summary>
        /// sets or gets the ShowEnableToggle
        /// </summary>
        public bool ShowEnableToggle
        {
        get { return (bool)this.GetValue(ShowEnableToggleProperty); }
        set { this.SetValue(ShowEnableToggleProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowEnableToggle
        /// </summary>
        public static readonly DependencyProperty ShowEnableToggleProperty = DependencyProperty.Register(
            "ShowEnableToggle",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'EnableToggle'
        /// <summary>
        /// sets or gets the ShowEnableToggle
        /// </summary>
        public bool EnableToggle
        {
            get { return (bool)this.GetValue(EnableToggleProperty); }
            set { this.SetValue(EnableToggleProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowEnableToggle
        /// </summary>
        public static readonly DependencyProperty EnableToggleProperty = DependencyProperty.Register(
            "EnableToggle",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(true)
        );
        #endregion

        #region DependencyProperty 'ShowCheckmark'
        /// <summary>
        /// sets or gets the ShowCheckmark
        /// </summary>
        public bool ShowCheckmark
        {
        get { return (bool)this.GetValue(ShowCheckmarkProperty); }
        set { this.SetValue(ShowCheckmarkProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ShowCheckmark
        /// </summary>
        public static readonly DependencyProperty ShowCheckmarkProperty = DependencyProperty.Register(
            "ShowCheckmark",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion
        
        
        #region DependencyProperty 'IsOn'
        /// <summary>
        /// sets or gets the IsOn
        /// </summary>
        public bool IsOn
        {
        get { return (bool)this.GetValue(IsOnProperty); }
        set { this.SetValue(IsOnProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsOn
        /// </summary>
        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register(
            "IsOn",
            typeof(bool),
            typeof(AccordeonItem),
            new PropertyMetadata(false)
        );
        #endregion
        
        #region enum 'AccordeonItemPosition'
        /// <summary>
        /// The enum AccordeonItemPosition
        /// </summary>
        public enum AccordeonItemPosition
        {
            Top,
            Center,
            Bottom,
            Single
        }
        #endregion
    }
}
