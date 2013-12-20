using Microsoft.Practices.Prism.Events;
using ReferenceApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReferenceApplication.Base
{
    public class BackgroundCommand : ICommand
    {
        private readonly Action<object> _backgroundExecute;
        private readonly Action<object> _completedExecute;
        private readonly Predicate<object> _canExecute;
        
        BackgroundWorker _worker;
        IEventAggregator eventAggregator;

        public BackgroundCommand(Action<object> backgroundExecute, Action<object> completedExecute, Predicate<object> canExecute)
        {
            _backgroundExecute = backgroundExecute;
            _completedExecute = completedExecute;
            _canExecute = canExecute;
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            eventAggregator = App.CurrentApp.EventAggregator;
        }
        
        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _completedExecute(null);
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _backgroundExecute(e.Argument);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _worker.RunWorkerAsync(parameter);
        }
    }
}
