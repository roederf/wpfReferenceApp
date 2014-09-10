
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
    public class EditViewModel : BaseViewModel
    {
        IApplicationModel _appModel;

        public EditViewModel(IApplicationModel appModel)
            :base()
        {
            _appModel = appModel;
            
            Name = _appModel.CurrentFile.Name;
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
        

        #region Property BusyState 'BusyState'
        private BusyState _BusyState = BusyState.Active;
        public BusyState BusyState
        {
            get { return _BusyState; }
            set
            {
                if (_BusyState != value)
                {
                    _BusyState = value;
                    OnPropertyChanged("BusyState");
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
                return _CloseFileCommand ?? (_CloseFileCommand = new BackgroundCommand(OnCloseFileCommand, OnCloseFileCompleted, CanCloseFile));
            }
        }

        private bool CanCloseFile(object param)
        {
            return true;
        }

        private void OnCloseFileCommand(DoWorkEventArgs param)
        {
            param.Result = _appModel.CloseFile();
        }

        private void OnCloseFileCompleted(RunWorkerCompletedEventArgs param)
        {
            if ((bool)param.Result)
            {
                this.PopViewModel();
            }
        }
        #endregion


    }
}
