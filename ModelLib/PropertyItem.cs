using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
    public class PropertyItem : BaseModel
    {
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


    }
}
