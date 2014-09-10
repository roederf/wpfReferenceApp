using ModelInterfaces;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class PropertyItemViewModel : BaseViewModel
    {
        public PropertyItemViewModel(IPropertyItem item)
        {
            Model = item as INotifyPropertyChanged;
        }

        protected override void initializeFromModel()
        {
            base.initializeFromModel();

            IPropertyItem item = Model as IPropertyItem;
            Name = item.Name;
            HasChanged = item.HasChanged;
        }

        protected override void OnModelPropertyChanged(string name)
        {
            base.OnModelPropertyChanged(name);

            IPropertyItem item = Model as IPropertyItem;
            Name = item.Name;
            HasChanged = item.HasChanged;
        }

        #region Property string 'Name'
        private string _Name = null;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");

                    IPropertyItem item = Model as IPropertyItem;
                    item.ChangeName(_Name);
                }
            }
        }
        #endregion

        #region Property bool 'HasChanged'
        private bool _HasChanged = false;
        public bool HasChanged
        {
            get { return _HasChanged; }
            set
            {
                if (_HasChanged != value)
                {
                    _HasChanged = value;
                    OnPropertyChanged("HasChanged");
                }
            }
        }
        #endregion
    }
}
