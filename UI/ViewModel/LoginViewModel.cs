
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

namespace UI
{
    public class LoginViewModel : BaseViewModel
    {
        private IApplicationModel _appModel;

        public LoginViewModel(IApplicationModel appModel)
            :base()
        {
            _appModel = appModel;
            Model = _appModel;
        }
        
        #region Command 'LoginCommand', Parameter: object
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _LoginCommand ?? (_LoginCommand = new BackgroundCommand(OnLoginCommand, OnLoginCommandCompleted, CanLogin));
            }
        }

        private bool CanLogin(object param)
        {
            return true;
        }

        private void OnLoginCommand(DoWorkEventArgs param)
        {
            param.Result = _appModel.Login();
        }

        private void OnLoginCommandCompleted(RunWorkerCompletedEventArgs param)
        {
            if ((bool)param.Result)
            {
                this.PushViewModel(new HomeViewModel(_appModel));
            }
        }
        #endregion
    }
}
