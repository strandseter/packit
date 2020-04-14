using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Packit.App
{
    public class NoParameterRelayCommand : ICommand
    {
        private Action executeDelegate = null;

        private Func<bool> canExecuteDelegate = null;

        public event EventHandler CanExecuteChanged = null;

        public NoParameterRelayCommand(Action execute)
        {
            executeDelegate = execute;
            canExecuteDelegate = () => { return true; };
        }

        public bool CanExecute(object parameter) => canExecuteDelegate();
        public void Execute(object parameter) => executeDelegate?.Invoke();
    }
}
