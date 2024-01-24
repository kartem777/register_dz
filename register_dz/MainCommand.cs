using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace register_dz
{
    public class MainCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object?> action;
        public MainCommand(Action<object?> action)
        {
            this.action = action;
        }

        bool ICommand.CanExecute(object? parameter) => true;

        void ICommand.Execute(object? parameter) => action?.Invoke(parameter);
    }
}