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
        void Init();

        void Login();

        void Logout();

        void OpenFile();

        void CloseFile();

        void CalculateValue(string propertyName);

        IFileItem CurrentFile { get; set; }

        bool UnsavedChanges { get; }

        ApplicationState State { get; }
    }
}
