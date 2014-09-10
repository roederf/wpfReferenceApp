using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReferenceApplication.Base
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">Action Type.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">Action type.</param>
        /// <param name="canExecute">Predicate type.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="name">It is string.</param>
        /// <param name="execute">It is Action type.</param>
        /// <param name="canExecute">It is predicate.</param>
        public RelayCommand(string name, Action<T> execute, Predicate<T> canExecute)
        {
            this.Name = name;
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        /// <summary>
        /// Can Execute Changed event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>It is Name.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// It is to CanExecute.
        /// </summary>
        /// <param name="parameter">It is parameter.</param>
        /// <returns>Returns <c>true</c>.</returns>
        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }

            try
            {
                return this._canExecute((T)parameter);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return true;
        }

        /// <summary>
        /// It is to Execute.
        /// </summary>
        /// <param name="parameter">It is parameter.</param>
        public void Execute(object parameter)
        {
            try
            {
                this._execute((T)parameter);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
