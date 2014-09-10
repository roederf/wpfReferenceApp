
using BusinessLogicInterface;
using Microsoft.Practices.Prism.Events;
using ModelInterfaces;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using UI.ViewModel;

namespace UI
{
    public class PropertyViewModel : BaseViewModel
    {
        private IApplicationModel _appModel;
        

        public PropertyViewModel(IApplicationModel appModel)
            :base()
        {
            _appModel = appModel;
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
                    _Items.Add(new PropertyItemViewModel(item));
                }
                Items.Refresh();
            }
        }

        #region Property ObservableCollection<PropertyItemViewModel> 'Items'
        private ObservableCollection<PropertyItemViewModel> _Items = new ObservableCollection<PropertyItemViewModel>();
        public ICollectionView Items
        {
            get { return CollectionViewSource.GetDefaultView(_Items); }
        }
        #endregion



        #region Property PropertyItemViewModel 'SelectedItem'
        private PropertyItemViewModel _SelectedItem = null;
        public PropertyItemViewModel SelectedItem
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
