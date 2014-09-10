using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceApplication.Base
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool synchronizeModel = true;
        private bool updatingModel = false;

        private static Stack<BaseViewModel> _viewModels = new Stack<BaseViewModel>();

        private static IEventAggregator eventAggregator = new EventAggregator();

        private static BaseViewModel currentShell;
        public static BaseViewModel CurrentShell 
        {
            get
            {
                return currentShell;
            }
            private set
            {
                if (currentShell != value)
                {
                    currentShell = value;
                    if (CurrentShellChanged != null)
                    {
                        CurrentShellChanged(null, new EventArgs());
                    }
                }
            }
        }

        public static IEventAggregator EventAggregator
        {
            get
            {
                return eventAggregator;
            }
        }

        public static event EventHandler CurrentShellChanged;
        
        protected BaseViewModel()
        {
        }
                
        #region Property INotifyPropertyChanged 'Model'
        private INotifyPropertyChanged _Model = null;
        public INotifyPropertyChanged Model
        {
            get { return _Model; }
            set
            {
                if (_Model != value)
                {
                    // detach callbacks
                    if (_Model != null)
                    {
                        _Model.PropertyChanged -= OnPropertyChanged;
                    }

                    _Model = value;

                    synchronizeModel = false;

                    initializeFromModel();

                    synchronizeModel = true;

                    // attach callbacks
                    if (_Model != null)
                    {
                        _Model.PropertyChanged += OnPropertyChanged;
                    }

                    OnPropertyChanged("Model");
                }
            }
        }
        #endregion

        public static void SetStartupShell(BaseViewModel viewModel)
        {
            CurrentShell = viewModel;
        }

        protected void PushViewModel(BaseViewModel viewModel)
        {
            if (CurrentShell != null)
            {
                _viewModels.Push(CurrentShell);
            }

            CurrentShell = viewModel;
        }

        protected void PopViewModel()
        {
            if (_viewModels.Count > 0)
            {
                var vm = _viewModels.Pop();

                CurrentShell = vm;
            }
        }

        virtual protected void initializeFromModel()
        {

        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // only raise some event if we are not currently updating our model
            if (!updatingModel)
            {
                // signal that we do not need to update the model
                synchronizeModel = false;

                OnModelPropertyChanged(e.PropertyName);

                // switch back (updating the model is default)
                synchronizeModel = true;
            }
        }

        virtual protected void OnModelPropertyChanged(string name)
        {

        }

        virtual protected void UpdateModel(string propertyName)
        {

        }

        protected void OnPropertyChanged(string name)
        {
            if (synchronizeModel)
            {
                updatingModel = true;

                UpdateModel(name);

                updatingModel = false;
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }

}
