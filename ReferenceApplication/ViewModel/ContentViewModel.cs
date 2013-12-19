using BusinessLogic;
using Microsoft.Practices.Prism.Events;
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
        ApplicationModel _appModel;
        PropertyItem _selectedItem = null;

        public ContentViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
            _appModel.PropertyChanged += _appModel_PropertyChanged;
            Model = _appModel.CurrentFile;

            EventAggregator.GetEvent<PropertySelectionChangedEvent>().Subscribe(OnPropertySelectionChanged);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            EventAggregator.GetEvent<PropertySelectionChangedEvent>().Unsubscribe(OnPropertySelectionChanged);
        }

        void _appModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                if (_appModel.State == ApplicationState.Edit_Progress)
                {
                    BusyState = ViewModel.BusyState.Busy;
                }
                else
                {
                    BusyState = ViewModel.BusyState.Active;
                }
            }
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

        #region Command 'DoSomethingCommand', Parameter: object
        private ICommand _DoSomethingCommand;
        public ICommand DoSomethingCommand
        {
            get
            {
                return _DoSomethingCommand ?? (_DoSomethingCommand = new RelayCommand<object>(OnDoSomethingCommand, CanDoSomething));
            }
        }

        private bool CanDoSomething(object param)
        {
            var pItem = _appModel.CurrentFile.Properties.FirstOrDefault(p => p.Name == SelectedPropertyName);
            return pItem != null;
        }

        private void OnDoSomethingCommand(object param)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
                {
                    _appModel.CalculateValue(SelectedPropertyName);
                };
            bw.RunWorkerCompleted += (s, e) =>
                {
                };
            bw.RunWorkerAsync();
        }
        #endregion
    }
}
