using System;
using System.Windows.Input;

namespace Packit.App.Helpers
{
    public class RelayCommand : ICommand
    {
        public Action Action { get; }
        public Func<bool> Func { get; }

        public event EventHandler CanExecuteChanged;

        public RelayCommand()
        {
        }

        public RelayCommand(Func<bool> canExecute)
        {
            Func = canExecute;
        }

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            Action = execute ?? throw new ArgumentNullException(nameof(execute));
            Func = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return Func == null || Func();
        }

        public virtual void Execute(object parameter) => Action();

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public Action<T> Action { get; }
        public Func<T, bool> Func { get; }

        public event EventHandler CanExecuteChanged;

        public RelayCommand()
        {
        }

        public RelayCommand(Func<T, bool> canExecute)
        {
            Func = canExecute;
        }

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            Action = execute ?? throw new ArgumentNullException(nameof(execute));
            Func = canExecute;
        }

        public bool CanExecute(object parameter) => Func == null || Func((T)parameter);

        public virtual void Execute(object parameter) => Action((T)parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
