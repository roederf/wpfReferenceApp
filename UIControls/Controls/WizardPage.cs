using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UIControls.Controls
{
    public class WizardPage : ContentControl
    {
        #region DependencyProperty 'Header'
        /// <summary>
        /// sets or gets the Header
        /// </summary>
        public object Header
        {
        get { return (object)this.GetValue(HeaderProperty); }
        set { this.SetValue(HeaderProperty, value); }
        }
        /// <summary>
        /// DependencyProperty Header
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(WizardPage),
            new PropertyMetadata(null)
        );
        #endregion
        
        #region DependencyProperty 'State'
        /// <summary>
        /// sets or gets the State
        /// </summary>
        public WizardPageState State
        {
        get { return (WizardPageState)this.GetValue(StateProperty); }
        set { this.SetValue(StateProperty, value); }
        }
        /// <summary>
        /// DependencyProperty State
        /// </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            "State",
            typeof(WizardPageState),
            typeof(WizardPage),
            new PropertyMetadata(WizardPageState.Empty)
        );
        #endregion
        
        #region DependencyProperty 'CanNext'
        /// <summary>
        /// sets or gets the CanNext
        /// </summary>
        public bool CanNext
        {
            get { return (bool)this.GetValue(CanNextProperty); }
            set { this.SetValue(CanNextProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CanNext
        /// </summary>
        public static readonly DependencyProperty CanNextProperty = DependencyProperty.Register(
            "CanNext",
            typeof(bool),
            typeof(WizardPage),
            new PropertyMetadata(true)
        );
        #endregion

        #region DependencyProperty 'CanBack'
        /// <summary>
        /// sets or gets the CanNext
        /// </summary>
        public bool CanBack
        {
            get { return (bool)this.GetValue(CanBackProperty); }
            set { this.SetValue(CanBackProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CanNext
        /// </summary>
        public static readonly DependencyProperty CanBackProperty = DependencyProperty.Register(
            "CanBack",
            typeof(bool),
            typeof(WizardPage),
            new PropertyMetadata(true)
        );
        #endregion

        #region DependencyProperty 'CanReset'
        /// <summary>
        /// sets or gets the CanReset
        /// </summary>
        public bool CanReset
        {
            get { return (bool)this.GetValue(CanResetProperty); }
            set { this.SetValue(CanResetProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CanNext
        /// </summary>
        public static readonly DependencyProperty CanResetProperty = DependencyProperty.Register(
            "CanReset",
            typeof(bool),
            typeof(WizardPage),
            new PropertyMetadata(true)
        );
        #endregion

        #region DependencyProperty 'CanSave'
        /// <summary>
        /// sets or gets the CanNext
        /// </summary>
        public bool CanSave
        {
            get { return (bool)this.GetValue(CanSaveProperty); }
            set { this.SetValue(CanSaveProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CanNext
        /// </summary>
        public static readonly DependencyProperty CanSaveProperty = DependencyProperty.Register(
            "CanSave",
            typeof(bool),
            typeof(WizardPage),
            new PropertyMetadata(false)
        );
        #endregion
    }

    #region enum 'WizardPageState'
    /// <summary>
    /// The enum WizardPageState
    /// </summary>
    public enum WizardPageState
    {
        Empty,
        Processing,
        Filled
    }
    #endregion
        
        
}
