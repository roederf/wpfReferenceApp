﻿using BusinessLogic;
using BusinessLogicInterface;
using BusinessLogicMockup;
using Microsoft.Practices.Prism.Events;
using ReferenceApplication.Base;
using ReferenceApplication.ViewModel;
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
using UI.Interfaces;

namespace ReferenceApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        IEventAggregator eventAggregator = new EventAggregator();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var am = new ApplicationModel();
            var am = new ApplicationMockup();
            am.PropertyChanged += AppModel_PropertyChanged;
            ApplicationModel = am;

            ViewModelFactory.SetViewmodelAssembly(Assembly.GetExecutingAssembly());
            ViewModelFactory.SetInterfaceAssembly(Assembly.Load(new AssemblyName("UI")));
            
            ViewModelFactory.RegisterClassesFromFolder();

            MainWindow mw = new MainWindow();
            mw.DataContext = this;
            
            Name = "Reference App";

            ApplicationModel.Init();

            mw.Show();
        }

        void AppModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                switch (ApplicationModel.State)
                {
                    case ApplicationState.Login:
                        Shell = new LoginViewModel();
                        break;
                    case ApplicationState.Home:
                        Shell = new HomeViewModel();
                        break;
                    case ApplicationState.Edit:
                        Shell = new EditViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
        
        public static App CurrentApp { get { return (App)Application.Current; } }

        public IEventAggregator EventAggregator
        {
            get
            {
                return eventAggregator;
            }
        }

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

        public IApplicationModel ApplicationModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
