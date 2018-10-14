using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployesDatabase
{
    class LambdaCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;


        public LambdaCommand(Action<object> execute, Func<object, bool> can_execute = null)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _CanExecute = can_execute;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
