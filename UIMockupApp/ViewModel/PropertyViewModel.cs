using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UI.Interfaces;
using UIMockupApp.Base;

namespace UIMockupApp.ViewModel
{
    public class PropertyViewModel : BaseViewModel, IPropertyViewModel
    {
        public PropertyViewModel()
            : base()
        {
            _Items.Add("Property 1");
            _Items.Add("Property 2");
            _Items.Add("Property 3");
            _Items.Add("Property 4");
            _Items.Add("Property 5");
            _Items.Add("Property 6");
            _Items.Add("Property 7");

            Items.Refresh();
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
                }
            }
        }
        #endregion

    }
}
