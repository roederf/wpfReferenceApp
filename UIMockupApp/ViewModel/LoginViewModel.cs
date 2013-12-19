using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Interfaces;
using UIMockupApp.Base;

namespace UIMockupApp.ViewModel
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        
        public LoginViewModel()
            : base()
        {
            
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
            
        }
        #endregion
    }
}
