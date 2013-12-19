using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI;
using UIMockupApp.Base;
using UIMockupApp.ViewModel;

namespace UIMockupApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        IEventAggregator eventAggregator = new EventAggregator();

        public static App CurrentApp { get { return (App)Application.Current; } }

        public IEventAggregator EventAggregator
        {
            get
            {
                return eventAggregator;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Property object 'Shell'
        private object _Shell = null;
        public object Shell
        {
            get { return _Shell; }
            private set
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ViewModelFactory.Register(typeof(UI.Interfaces.IPropertyViewModel), typeof(PropertyViewModel));
            
            MainWindow mw = new MainWindow();
            mw.DataContext = this;
            
            mw.Show();
        }

        #region Command 'ShowLoginCommand', Parameter: object
        private ICommand _ShowLoginCommand;
        public ICommand ShowLoginCommand
        {
            get
            {
                return _ShowLoginCommand ?? (_ShowLoginCommand = new RelayCommand<object>(OnShowLoginCommand, CanShowLogin));
            }
        }

        private bool CanShowLogin(object param)
        {
            return true;
        }

        private void OnShowLoginCommand(object param)
        {
            Shell = new LoginViewModel();
        }
        #endregion

        #region Command 'ShowHomeCommand', Parameter: object
        private ICommand _ShowHomeCommand;
        public ICommand ShowHomeCommand
        {
            get
            {
                return _ShowHomeCommand ?? (_ShowHomeCommand = new RelayCommand<object>(OnShowHomeCommand, CanShowHome));
            }
        }

        private bool CanShowHome(object param)
        {
            return true;
        }

        private void OnShowHomeCommand(object param)
        {
            Shell = new HomeViewModel();
        }
        #endregion

        #region Command 'ShowEditCommand', Parameter: object
        private ICommand _ShowEditCommand;
        public ICommand ShowEditCommand
        {
            get
            {
                return _ShowEditCommand ?? (_ShowEditCommand = new RelayCommand<object>(OnShowEditCommand, CanShowEdit));
            }
        }

        private bool CanShowEdit(object param)
        {
            return true;
        }

        private void OnShowEditCommand(object param)
        {
            Shell = new EditViewModel();
        }
        #endregion
    }
}
