using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicMockup
{
    public class PropertyItem : IPropertyItem
    {
        public string Name
        {
            get;
            set;
        }

        public double Value
        {
            get;
            set;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
