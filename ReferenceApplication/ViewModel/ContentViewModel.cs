using BusinessLogic;
using BusinessLogicInterface;
using Microsoft.Practices.Prism.Events;
using ModelInterfaces;
using ModelLib;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Interfaces;

namespace ReferenceApplication.ViewModel
{
    public class ContentViewModel : BaseViewModel, IContentViewModel
    {
        IApplicationModel _appModel;
        IPropertyItem _selectedItem = null;

        public ContentViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
            Model = _appModel.CurrentFile;

            EventAggregator.GetEvent<PropertySelectionChangedEvent>().Subscribe(OnPropertySelectionChanged);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            EventAggregator.GetEvent<PropertySelectionChangedEvent>().Unsubscribe(OnPropertySelectionChanged);
        }
        
        void OnPropertySelectionChanged(string payload)
        {
            if (_selectedItem != null)
            {
                _selectedItem.PropertyChanged -= _selectedItem_PropertyChanged;
            }

            var pItem = _appModel.CurrentFile.Properties.FirstOrDefault(p => p.Name == payload);
            _selectedItem = pItem;

            if (pItem != null)
            {
                
                _selectedItem.PropertyChanged += _selectedItem_PropertyChanged;
                SelectedPropertyName = pItem.Name;
                Value = pItem.Value.ToString();
            }
        }

        void _selectedItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                Value = _selectedItem.Value.ToString();
            }
        }
        
        #region Property string 'Value'
        private string _Value = null;
        public string Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        #endregion

        #region Property string 'SelectedPropertyName'
        private string _SelectedPropertyName = null;
        public string SelectedPropertyName
        {
            get { return _SelectedPropertyName; }
            set
            {
                if (_SelectedPropertyName != value)
                {
                    _SelectedPropertyName = value;
                    OnPropertyChanged("SelectedPropertyName");
                }
            }
        }
        #endregion
                        
        #region Property BusyState 'BusyState'
        private BusyState _BusyState = BusyState.Active;
        public BusyState BusyState
        {
            get { return _BusyState; }
            set
            {
                if (_BusyState != value)
                {
                    _BusyState = value;
                    OnPropertyChanged("BusyState");
                }
            }
        }
        #endregion

        #region Command 'DoSomethingInBackgroundCommand', Parameter: object
        private ICommand _DoSomethingInBackgroundCommand;
        public ICommand DoSomethingInBackgroundCommand
        {
            get
            {
                return _DoSomethingInBackgroundCommand ?? (_DoSomethingInBackgroundCommand = new BackgroundCommand(OnDoSomethingInBackgroundCommand, OnDoSomethingInBackgroundCompleted, CanDoSomethingInBackground));
            }
        }

        private bool CanDoSomethingInBackground(object param)
        {
            var pItem = _appModel.CurrentFile.Properties.FirstOrDefault(p => p.Name == SelectedPropertyName);
            return pItem != null;
        }

        private void OnDoSomethingInBackgroundCommand(object param)
        {
            _appModel.CalculateValue(SelectedPropertyName);
        }

        private void OnDoSomethingInBackgroundCompleted(object param)
        {
        }
        #endregion

    }
}
