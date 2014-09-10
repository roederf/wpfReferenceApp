using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelLib
{
    [Serializable]
    public class PropertyItem : BaseModel, IPropertyItem
    {
        public void ChangeName(string name)
        {
            if (this.Name != name)
            {
                Name = name;
                HasChanged = true;
            }
        }

        #region Property double 'Value'
        private double _Value = 0.0;
        public double Value
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
