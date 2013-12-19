using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelInterfaces
{
    public interface IFileItem : INotifyPropertyChanged
    {
        string Name { get; set; }

        List<IPropertyItem> Properties { get; }
    }
}
