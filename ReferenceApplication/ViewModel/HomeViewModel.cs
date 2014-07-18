using BusinessLogic;
using BusinessLogicInterface;
using Microsoft.Practices.Prism.Events;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReferenceApplication.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        IApplicationModel _appModel;

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
                return _LogoutCommand ?? (_LogoutCommand = new BackgroundCommand(OnLogoutCommand, OnLogoutCompleted, CanLogout));
            }
        }

        private bool CanLogout(object param)
        {
            return true;
        }

        private void OnLogoutCommand(DoWorkEventArgs param)
        {
            param.Result = _appModel.Logout();
        }

        private void OnLogoutCompleted(RunWorkerCompletedEventArgs param)
        {
            if ((bool)param.Result)
            {
                this.PopViewModel();
            }
        }
        #endregion

        #region Command 'OpenFileCommand', Parameter: object
        private ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get
            {
                return _OpenFileCommand ?? (_OpenFileCommand = new BackgroundCommand(OnOpenFileCommand, OnOpenFileCompleted, CanOpenFile));
            }
        }

        private bool CanOpenFile(object param)
        {
            return true;
        }

        private void OnOpenFileCommand(DoWorkEventArgs param)
        {
            param.Result = _appModel.OpenFile();
        }

        private void OnOpenFileCompleted(RunWorkerCompletedEventArgs param)
        {
            if ((bool)param.Result)
            {
                this.PushViewModel(new EditViewModel());
            }
        }
        #endregion
    }
}
