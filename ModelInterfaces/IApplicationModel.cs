using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicInterface
{
    public interface IApplicationModel : INotifyPropertyChanged
    {
        bool Login();

        bool Logout();

        bool OpenFile();

        bool SaveFile();

        bool CloseFile();

        void CalculateValue(string propertyName);

        IFileItem CurrentFile { get; set; }
    }
}
