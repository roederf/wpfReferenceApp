using BusinessLogic;
using Microsoft.Practices.Prism.Events;
using ModelLib;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UI.Interfaces;

namespace ReferenceApplication.ViewModel
{
    public class PropertyViewModel : BaseViewModel, IPropertyViewModel
    {
        private ApplicationModel _appModel;
        

        public PropertyViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
            Model = _appModel.CurrentFile;
            _appModel.PropertyChanged += _appModel_PropertyChanged;
        }

        void _appModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                if (_appModel.State == ApplicationState.Edit_Progress)
                {
                    BusyState = ViewModel.BusyState.Inactive;
                }
                else
                {
                    BusyState = ViewModel.BusyState.Active;
                }
            }
        }
        
        protected override void initializeFromModel()
        {
            base.initializeFromModel();

            if (Model != null)
            {
                FileItem fileItem = Model as FileItem;

                _Items.Clear();
                foreach (var item in fileItem.Properties)
                {
                    _Items.Add(item.Name);
                }
                Items.Refresh();
            }
        }

        #region Property ObservableCollection<string> 'Items'
        private ObservableCollection<string> _Items = new ObservableCollection<string>();
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_Items); }
        }
        #endregion

        #region Property string 'SelectedItem'
        private string _SelectedItem = null;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged("SelectedItem");

                    EventAggregator.GetEvent<PropertySelectionChangedEvent>().Publish(value);
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
    }
}
