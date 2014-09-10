using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelInterfaces
{
    public interface IPropertyItem : INotifyPropertyChanged
    {
        string Name { get; set; }

        double Value { get; set; }

        bool HasChanged { get; set; }

        void ChangeName(string name);
    }
}
