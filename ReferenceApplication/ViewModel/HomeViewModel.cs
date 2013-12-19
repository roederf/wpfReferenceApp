using BusinessLogic;
using Microsoft.Practices.Prism.Events;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReferenceApplication.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        ApplicationModel _appModel;

        public HomeViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
        }

        #region Command 'LogoutCommand', Parameter: object
        private ICommand _LogoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return _LogoutCommand ?? (_LogoutCommand = new RelayCommand<object>(OnLogoutCommand, CanLogout));
            }
        }

        private bool CanLogout(object param)
        {
            return true;
        }

        private void OnLogoutCommand(object param)
        {
            _appModel.Logout();
        }
        #endregion

        #region Command 'OpenFileCommand', Parameter: object
        private ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get
            {
                return _OpenFileCommand ?? (_OpenFileCommand = new RelayCommand<object>(OnOpenFileCommand, CanOpenFile));
            }
        }

        private bool CanOpenFile(object param)
        {
            return true;
        }

        private void OnOpenFileCommand(object param)
        {
            _appModel.OpenFile();
        }
        #endregion
    }
}
