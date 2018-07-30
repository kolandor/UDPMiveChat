using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UDPMiveChat
{
    class CommandExecutor : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<object, Task> executingFunction;
        private bool isExecuting;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (isExecuting)
                return;

            isExecuting = true;
            await executingFunction(parameter);
            isExecuting = false;
        }


        public static CommandExecutor ExecuteFunction(Func<Task> func)
        {
            var executableCommand = new CommandExecutor
            {
                executingFunction = (obj) =>
                {
                    return func();
                }
            };
            return executableCommand;
        }

        public static CommandExecutor ExecuteFunction(Func<object, Task> func)
        {
            var executableCommand = new CommandExecutor
            {
                executingFunction = func
            };
            return executableCommand;
        }

        public static CommandExecutor ExecuteFunction<T>(Func<T, Task> func)
        {
            var executableCommand = new CommandExecutor
            {
                executingFunction = (object obj) =>
                {
                    var objT = default(T);
                    objT = (T)obj;
                    return func(objT);
                }
            };

            return executableCommand;
        }
    }
}

