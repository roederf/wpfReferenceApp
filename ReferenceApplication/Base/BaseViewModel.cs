﻿using Microsoft.Practices.Prism.Events;
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
        
        protected BaseViewModel()
        {
            EventAggregator = App.CurrentApp.EventAggregator;
        }

        #region Property IEventAggregator 'EventAggregator'
        private IEventAggregator _EventAggregator = null;
        protected IEventAggregator EventAggregator
        {
            get { return _EventAggregator; }
            private set
            {
                if (_EventAggregator != value)
                {
                    _EventAggregator = value;
                }
            }
        }
        #endregion
        
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

        #region Property TransitionType 'Transition'
        private TransitionType _Transition = TransitionType.Blend;
        public TransitionType Transition
        {
            get { return _Transition; }
            set
            {
                if (_Transition != value)
                {
                    _Transition = value;
                    OnPropertyChanged("Transition");
                }
            }
        }
        #endregion
        

        protected void PushViewModel(BaseViewModel viewModel)
        {
            _viewModels.Push(App.CurrentApp.Shell as BaseViewModel);

            App.CurrentApp.Shell = viewModel;
        }

        protected void PopViewModel()
        {
            if (_viewModels.Count > 0)
            {
                var vm = _viewModels.Pop();

                App.CurrentApp.Shell = vm;
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

    public enum TransitionType
    {
        Slide,
        Blend
    }
}
