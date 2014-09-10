using Microsoft.Practices.Prism.Events;
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
        private readonly Action<DoWorkEventArgs> _backgroundExecute;
        private readonly Action<RunWorkerCompletedEventArgs> _completedExecute;
        private readonly Predicate<object> _canExecute;
        
        BackgroundWorker _worker;
        IEventAggregator eventAggregator;

        public static int BusyCount {get; private set;}

        public static event EventHandler BusyCountChanged;
        
        public BackgroundCommand(Action<DoWorkEventArgs> backgroundExecute, Action<RunWorkerCompletedEventArgs> completedExecute, Predicate<object> canExecute)
        {
            _backgroundExecute = backgroundExecute;
            _completedExecute = completedExecute;
            _canExecute = canExecute;
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            eventAggregator = BaseViewModel.EventAggregator;
        }
        
        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BusyCount--;
            if (BusyCountChanged != null)
            {
                BusyCountChanged(this, new EventArgs());
            }
            _completedExecute(e);
        }

        void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BusyCount++;
            if (BusyCountChanged != null)
            {
                BusyCountChanged(this, new EventArgs());
            }
            _backgroundExecute(e);
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
