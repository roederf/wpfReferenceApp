using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIMockupApp.Base;

namespace UIMockupApp.ViewModel
{
    public class EditViewModel : BaseViewModel
    {
        public EditViewModel()
            : base()
        {
            Name = "Testfilename";
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
                }
            }
        }
        #endregion
        
        #region Command 'CloseFileCommand', Parameter: object
        private ICommand _CloseFileCommand;
        public ICommand CloseFileCommand
        {
            get
            {
                return _CloseFileCommand ?? (_CloseFileCommand = new RelayCommand<object>(OnCloseFileCommand, CanCloseFile));
            }
        }

        private bool CanCloseFile(object param)
        {
            return true;
        }

        private void OnCloseFileCommand(object param)
        {
            
        }
        #endregion


    }
}
