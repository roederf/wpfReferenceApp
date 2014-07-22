using BusinessLogic;
using BusinessLogicInterface;
using Microsoft.Practices.Prism.Events;
using ModelInterfaces;
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
        private IApplicationModel _appModel;
        

        public PropertyViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
            Model = _appModel.CurrentFile;
        }
                
        protected override void initializeFromModel()
        {
            base.initializeFromModel();

            if (Model != null)
            {
                IFileItem fileItem = Model as IFileItem;

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

    }
}
