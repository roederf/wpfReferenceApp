using BusinessLogic;
using Microsoft.Practices.Prism.Events;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Interfaces;

namespace ReferenceApplication.ViewModel
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private ApplicationModel _appModel;

        public LoginViewModel()
            :base()
        {
            _appModel = App.CurrentApp.ApplicationModel;
            Model = _appModel;
        }

        #region Command 'LoginCommand', Parameter: object
        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _LoginCommand ?? (_LoginCommand = new RelayCommand<object>(OnLoginCommand, CanLogin));
            }
        }

        private bool CanLogin(object param)
        {
            return true;
        }

        private void OnLoginCommand(object param)
        {
            _appModel.Login();
        }
        #endregion
    }
}
