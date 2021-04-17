using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressBar.Utility
{
    public class DelegateCommand : ICommand
    {
        readonly Action _execute;
        readonly Func<bool> _canExecute;

        public DelegateCommand(Action OnExecute, Func<bool> OnCanExecute = null)
        {
            _execute = OnExecute;
            _canExecute = OnCanExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute?.Invoke();

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
    }

    public class DelegateParameterCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Func<bool> _canExecute;

        public DelegateParameterCommand(Action<object> OnExecute, Func<bool> OnCanExecute)
        {
            _execute = OnExecute;
            _canExecute = OnCanExecute;
        }

        public void Execute(object parameter) => _execute?.Invoke(parameter);

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
    }
}
