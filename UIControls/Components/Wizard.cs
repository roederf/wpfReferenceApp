using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UIControls.Base;
using UIControls.Controls;

namespace UIControls.Components
{
    public class Wizard : ItemsControl
    {
        WizardPagesCollection pagesCollection = new WizardPagesCollection();

        private int NumPages = 0;
        private int CurrentPage = 0;

        public Wizard()
        {

        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is WizardPage;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new WizardPage();
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (Items.Count != 0)
            {
                if (!IsItemItsOwnContainerOverride(Items[CurrentPage]))
                {
                    SelectedPage = new WizardPage()
                    {
                        DataContext = Items[CurrentPage],
                        Style = this.ItemContainerStyle,
                        ContentTemplateSelector = this.ItemTemplateSelector
                    };

                    SelectedItem = Items[CurrentPage];
                }
                else
                {
                    SelectedPage = Items[CurrentPage];
                    SelectedItem = ((WizardPage)Items[CurrentPage]).DataContext;
                }

                NumPages = Items.Count;

                if (CurrentPage == (NumPages - 1))
                {
                    IsOnLastPage = true;
                   // ((WizardPage)Items[CurrentPage]).CanSave = true;
                }
            }
            else
            {
                CurrentPage = 0;
                SelectedIndex = 0;
                IsOnLastPage = false;
                if(Items.Count>0)
                ((WizardPage)Items[CurrentPage]).CanSave = false;
            }
        }

        protected void UpdatePage()
        {
            if (Items.Count != 0)
            {
                if (!IsItemItsOwnContainerOverride(Items[CurrentPage]))
                {
                    SelectedPage = new WizardPage()
                    {
                        DataContext = Items[CurrentPage],
                        Style = this.ItemContainerStyle,
                        ContentTemplateSelector = this.ItemTemplateSelector
                    };

                    SelectedItem = Items[CurrentPage];
                }
                else
                {
                    SelectedPage = Items[CurrentPage];
                    SelectedItem = ((WizardPage)Items[CurrentPage]).DataContext;
                }
            }
        }

        #region DependencyProperty 'ItemTemplate'
        /// <summary>
        /// sets or gets the ItemTemplate
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// DependencyProperty ItemTemplate
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'IsOnLastPage'
        /// <summary>
        /// sets or gets the LastPage
        /// </summary>
        public bool IsOnLastPage
        {
            get { return (bool)this.GetValue(IsOnLastPageProperty); }
            set { this.SetValue(IsOnLastPageProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LastPage
        /// </summary>
        public static readonly DependencyProperty IsOnLastPageProperty = DependencyProperty.Register(
            "IsOnLastPage",
            typeof(bool),
            typeof(Wizard),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'SelectedItem'
        /// <summary>
        /// sets or gets the SelectedItem
        /// </summary>
        public object SelectedItem
        {
            get { return (object)this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedItem
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'SelectedPage'
        /// <summary>
        /// sets or gets the SelectedPage
        /// </summary>
        public object SelectedPage
        {
            get { return (object)this.GetValue(SelectedPageProperty); }
            set { this.SetValue(SelectedPageProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedPage
        /// </summary>
        public static readonly DependencyProperty SelectedPageProperty = DependencyProperty.Register(
            "SelectedPage",
            typeof(object),
            typeof(Wizard),
            new PropertyMetadata(null, SelectedPagePropertyChangedCallback)
        );
        private static void SelectedPagePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Wizard _this = sender as Wizard;
            if (_this != null)
            {

            }
        }
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
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'FinishCommand'
        /// <summary>
        /// sets or gets the FinishCommand
        /// </summary>
        public ICommand FinishCommand
        {
            get { return (ICommand)this.GetValue(FinishCommandProperty); }
            set { this.SetValue(FinishCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty FinishCommand
        /// </summary>
        public static readonly DependencyProperty FinishCommandProperty = DependencyProperty.Register(
            "FinishCommand",
            typeof(ICommand),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'NextCommand'
        /// <summary>
        /// sets or gets the NextCommand
        /// </summary>
        public ICommand NextCommand
        {
            get { return (ICommand)this.GetValue(NextCommandProperty); }
            set { this.SetValue(NextCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty NextCommand
        /// </summary>
        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register(
            "NextCommand",
            typeof(ICommand),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'PreviousCommand'
        /// <summary>
        /// sets or gets the PreviousCommand
        /// </summary>
        public ICommand PreviousCommand
        {
            get { return (ICommand)this.GetValue(PreviousCommandProperty); }
            set { this.SetValue(PreviousCommandProperty, value); }
        }
        /// <summary>
        /// DependencyProperty PreviousCommand
        /// </summary>
        public static readonly DependencyProperty PreviousCommandProperty = DependencyProperty.Register(
            "PreviousCommand",
            typeof(ICommand),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'NavigationFreeze'
        /// <summary>
        /// sets or gets the NavigationFreeze
        /// </summary>
        public bool NavigationFreeze
        {
            get { return (bool)this.GetValue(NavigationFreezeProperty); }
            set { this.SetValue(NavigationFreezeProperty, value); }
        }
        /// <summary>
        /// DependencyProperty NavigationFreeze
        /// </summary>
        public static readonly DependencyProperty NavigationFreezeProperty = DependencyProperty.Register(
            "NavigationFreeze",
            typeof(bool),
            typeof(Wizard),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'LabelPrevious'
        /// <summary>
        /// sets or gets the LabelPrevious
        /// </summary>
        public string LabelPrevious
        {
            get { return (string)this.GetValue(LabelPreviousProperty); }
            set { this.SetValue(LabelPreviousProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LabelPrevious
        /// </summary>
        public static readonly DependencyProperty LabelPreviousProperty = DependencyProperty.Register(
            "LabelPrevious",
            typeof(string),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'LabelCancel'
        /// <summary>
        /// sets or gets the LabelCancel
        /// </summary>
        public string LabelCancel
        {
            get { return (string)this.GetValue(LabelCancelProperty); }
            set { this.SetValue(LabelCancelProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LabelCancel
        /// </summary>
        public static readonly DependencyProperty LabelCancelProperty = DependencyProperty.Register(
            "LabelCancel",
            typeof(string),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'LabelNext'
        /// <summary>
        /// sets or gets the LabelNext
        /// </summary>
        public string LabelNext
        {
            get { return (string)this.GetValue(LabelNextProperty); }
            set { this.SetValue(LabelNextProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LabelNext
        /// </summary>
        public static readonly DependencyProperty LabelNextProperty = DependencyProperty.Register(
            "LabelNext",
            typeof(string),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'LabelFinish'
        /// <summary>
        /// sets or gets the LabelFinish
        /// </summary>
        public string LabelFinish
        {
            get { return (string)this.GetValue(LabelFinishProperty); }
            set { this.SetValue(LabelFinishProperty, value); }
        }
        /// <summary>
        /// DependencyProperty LabelFinish
        /// </summary>
        public static readonly DependencyProperty LabelFinishProperty = DependencyProperty.Register(
            "LabelFinish",
            typeof(string),
            typeof(Wizard),
            new PropertyMetadata(null)
        );
        #endregion

        #region DependencyProperty 'CurrentStep'
        /// <summary>
        /// sets or gets the CurrentStep
        /// </summary>
        public int CurrentStep
        {
            get { return (int)this.GetValue(CurrentStepProperty); }
            set { this.SetValue(CurrentStepProperty, value); }
        }
        /// <summary>
        /// DependencyProperty CurrentStep
        /// </summary>
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register(
            "CurrentStep",
            typeof(int),
            typeof(Wizard),
            new PropertyMetadata(1)
        );
        #endregion

        #region DependencyProperty 'StepCount'
        /// <summary>
        /// sets or gets the StepCount
        /// </summary>
        public int StepCount
        {
        get { return (int)this.GetValue(StepCountProperty); }
        set { this.SetValue(StepCountProperty, value); }
        }
        /// <summary>
        /// DependencyProperty StepCount
        /// </summary>
        public static readonly DependencyProperty StepCountProperty = DependencyProperty.Register(
            "StepCount",
            typeof(int),
            typeof(Wizard),
            new PropertyMetadata(0)
        );
        #endregion

        #region DependencyProperty 'StepsVisible'
        /// <summary>
        /// sets or gets the StepCount
        /// </summary>
        public bool StepsVisible
        {
            get { return (bool)this.GetValue(StepsVisibleProperty); }
            set { this.SetValue(StepsVisibleProperty, value); }
        }
        /// <summary>
        /// DependencyProperty StepCount
        /// </summary>
        public static readonly DependencyProperty StepsVisibleProperty = DependencyProperty.Register(
            "StepsVisible",
            typeof(bool),
            typeof(Wizard),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'SelectedIndex'
        /// <summary>
        /// sets or gets the SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedIndex
        /// </summary>
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex",
            typeof(int),
            typeof(Wizard),
            new PropertyMetadata(0, SelectedIndexPropertyChangedCallback)
        );
        private static void SelectedIndexPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Wizard _this = sender as Wizard;
            if (_this != null)
            {
                _this.CurrentPage = (int)e.NewValue;
                _this.CurrentStep = _this.CurrentPage + 1;
                _this.UpdatePage();
            }
        }
        #endregion

        #region DependencyProperty 'ResetWizard'
        /// <summary>
        /// sets or gets the ResetWizard
        /// </summary>
        public bool ResetWizard
        {
            get { return (bool)this.GetValue(ResetWizardProperty); }
            set { this.SetValue(ResetWizardProperty, value); }
        }
        /// <summary>
        /// DependencyProperty SelectedIndex
        /// </summary>
        public static readonly DependencyProperty ResetWizardProperty = DependencyProperty.Register(
            "ResetWizard",
            typeof(bool),
            typeof(Wizard),
            new PropertyMetadata(false)
        );
        #endregion

        #region DependencyProperty 'IsOfflineSaveEnabled'
        /// <summary>
        /// sets or gets the IsOfflineSaveEnabled
        /// </summary>
        public bool IsOfflineSaveEnabled
        {
            get { return (bool)this.GetValue(IsOfflineSaveEnabledProperty); }
            set { this.SetValue(IsOfflineSaveEnabledProperty, value); }
        }
        /// <summary>
        /// DependencyProperty IsOfflineSaveEnabled
        /// </summary>
        public static readonly DependencyProperty IsOfflineSaveEnabledProperty = DependencyProperty.Register(
            "IsOfflineSaveEnabled",
            typeof(bool),
            typeof(Wizard),
            new PropertyMetadata(false)
        );
        #endregion

        #region Command 'InternalNextCommand', Parameter: object
        private ICommand _InternalNextCommand;
        public ICommand InternalNextCommand
        {
            get
            {
                return _InternalNextCommand ?? (_InternalNextCommand = new RelayCommand<object>(OnInternalNextCommand, CanInternalNext));
            }
        }

        private bool CanInternalNext(object param)
        {
            bool returnval = false;

            WizardPage wp = null;

            if (SelectedPage != null)
            {
                if (SelectedPage is WizardPage)
                {
                    wp = (WizardPage)SelectedPage;
                }
            }

            if (wp != null)
            {
                if (SelectedPage != null)
                {
                    if (wp.CanNext) returnval = true;
                }
            }

            return returnval;
        }

        private void OnInternalNextCommand(object param)
        {
            if (CurrentPage < (NumPages - 1))
            {
                CurrentPage++;

                SelectedIndex = CurrentPage;

                //UpdatePage();
            }

            if (CurrentPage == (NumPages - 1))
            {
                IsOnLastPage = true;
            }

            if (NextCommand != null)
            {
                NextCommand.Execute(this);
            }
        }
        #endregion

        #region Command 'InternalPreviousCommand', Parameter: object
        private ICommand _InternalPreviousCommand;
        public ICommand InternalPreviousCommand
        {
            get
            {
                return _InternalPreviousCommand ?? (_InternalPreviousCommand = new RelayCommand<object>(OnInternalPreviousCommand, CanInternalPrevious));
            }
        }

        private bool CanInternalPrevious(object param)
        {
            bool returnval = true;

            WizardPage wp = null;

            if (SelectedPage != null)
            {
                if (SelectedPage is WizardPage)
                {
                    wp = (WizardPage)SelectedPage;
                }
            }

            if (wp != null)
            {
                if (SelectedPage != null)
                {
                    if (!wp.CanBack) returnval = false;
                }
            }
            
            return CurrentPage > 0 && returnval;
        }

        private void OnInternalPreviousCommand(object param)
        {
            if (CurrentPage > 0)
            {
                if (IsOnLastPage)
                {
                    IsOnLastPage = false;
                }

                CurrentPage--;

                SelectedIndex = CurrentPage;

                //UpdatePage();

                if (PreviousCommand != null)
                {
                    PreviousCommand.Execute(this);
                }
            }
        }
        #endregion

        #region Command 'InternalCancelCommand', Parameter: object
        private ICommand _InternalCancelCommand;
        public ICommand InternalCancelCommand
        {
            get
            {
                return _InternalCancelCommand ?? (_InternalCancelCommand = new RelayCommand<object>(OnInternalCancelCommand, CanInternalCancel));
            }
        }

        private bool CanInternalCancel(object param)
        {
            bool returnval = false;

            WizardPage wp = null;

            if (SelectedPage != null)
            {
                if (SelectedPage is WizardPage)
                {
                    wp = (WizardPage)SelectedPage;
                }
            }

            if (wp != null)
            {
                if (SelectedPage != null)
                {
                    if (wp.CanReset) returnval = true;
                }
            }

            return returnval;
        }

        private void OnInternalCancelCommand(object param)
        {
            //Set  ResetWizard value to true to reset the wizard from intial page on click of cancel.
            if (ResetWizard)
            {
                IsOnLastPage = false;
                CurrentPage = 0;
                SelectedIndex = 0;
            }

            if (!IsItemItsOwnContainerOverride(Items[CurrentPage]))
            {
                SelectedPage = new WizardPage()
                {
                    DataContext = Items[CurrentPage],
                    Style = this.ItemContainerStyle,
                    ContentTemplateSelector = this.ItemTemplateSelector
                };

                SelectedItem = Items[CurrentPage];
            }
            else
            {
                SelectedPage = Items[CurrentPage];
                SelectedItem = ((WizardPage)Items[CurrentPage]).DataContext;
            }

            if (CancelCommand != null)
            {
                CancelCommand.Execute(this);
            }
        }
        #endregion

        #region Command 'InternalFinishCommand', Parameter: object
        private ICommand _InternalFinishCommand;
        public ICommand InternalFinishCommand
        {
            get
            {
                return _InternalFinishCommand ?? (_InternalFinishCommand = new RelayCommand<object>(OnInternalFinishCommand, CanInternalFinishCommand));
            }
        }

        private bool CanInternalFinishCommand(object param)
        {

            bool returnval = false;

            WizardPage wp = null;

            if (SelectedPage != null)
            {
                if (SelectedPage is WizardPage)
                {
                    wp = (WizardPage)SelectedPage;
                }
            }

            if (wp != null)
            {
                if (SelectedPage != null)
                {
                    if (wp.CanSave) returnval = true;
                }
            }

            return returnval;
            //return true;
        }

        private void OnInternalFinishCommand(object param)
        {
            //CurrentPage = 0;
            //IsOnLastPage = false;

            if (!IsItemItsOwnContainerOverride(Items[CurrentPage]))
            {
                SelectedPage = new WizardPage()
                {
                    DataContext = Items[CurrentPage],
                    Style = this.ItemContainerStyle,
                    ContentTemplateSelector = this.ItemTemplateSelector
                };

                SelectedItem = Items[CurrentPage];
            }
            else
            {
                SelectedPage = Items[CurrentPage];
                SelectedItem = ((WizardPage)Items[CurrentPage]).DataContext;
            }
            //Set  ResetWizard value to true to reset the wizard from intial page on click of cancel.
            if (ResetWizard)
            {
                IsOnLastPage = false;
                CurrentPage = 0;
                SelectedIndex = 0;
            }
            if (FinishCommand != null)
            {
                FinishCommand.Execute(this);
            }
        }
        #endregion
    }

    /// <summary>
    /// Wizard pages collection.
    /// </summary>
    public class WizardPagesCollection : ObservableCollection<WizardPage>
    {

    }
}
