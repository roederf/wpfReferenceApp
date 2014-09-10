using BusinessLogic;
using BusinessLogicInterface;
using Microsoft.Practices.Prism.Events;
using ReferenceApplication.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI;

namespace ReferenceApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var am = new ApplicationModel();
            //var am = new ApplicationMockup();
            ApplicationModel = am;

            //ViewModelFactory.RegisterInterfacesAndImplementations(Assembly.GetExecutingAssembly(), "ReferenceApplication.ViewModel", Assembly.Load(new AssemblyName("UI")), "UI.Interfaces");
            ViewModelFactory.RegisterViewModelParameter(ApplicationModel);

            BackgroundCommand.BusyCountChanged += BackgroundCommand_BusyCountChanged;
            BaseViewModel.CurrentShellChanged += BaseViewModel_CurrentShellChanged;

            MainWindow mw = new MainWindow();
            mw.DataContext = this;
            
            Name = "Reference App";

            BaseViewModel.SetStartupShell(new LoginViewModel(am));

            mw.Show();
        }

        void BaseViewModel_CurrentShellChanged(object sender, EventArgs e)
        {
            Shell = BaseViewModel.CurrentShell;
        }

        void BackgroundCommand_BusyCountChanged(object sender, EventArgs e)
        {
            IsBusy = BackgroundCommand.BusyCount > 0;
        }

        public static App CurrentApp { get { return (App)Application.Current; } }

        

        #region Property string 'Name'
        private string _Name = null;
        public string Name
        {
            get { return _Name; }
            private set
            {
                if (_Name != value)
                {
                    _Name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }
        #endregion

        #region Property object 'Shell'
        private object _Shell = null;
        public object Shell
        {
            get { return _Shell; }
            set
            {
                if (_Shell != value)
                {
                    _Shell = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Shell"));
                    }
                }
            }
        }
        #endregion

        #region Property bool 'IsBusy'
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                    }
                }
            }
        }
        #endregion
        
        public IApplicationModel ApplicationModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
