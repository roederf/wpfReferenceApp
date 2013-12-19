using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicMockup
{
    public class FileItem : IFileItem
    {
        public string Name
        {
            get;
            set;
        }

        public List<IPropertyItem> Properties
        {
            get;
            set;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
