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
        public CommandExecutor(Action action)
        {
            executableAction = action;
        }
        public event EventHandler CanExecuteChanged;

        // Action which is provided by your viewmodel and will be executed
        private Action executableAction;

        //propeprty that tells us if the action is still running
        private bool isExecuting;

        public bool CanExecute(object parameter) => true;

        // This method is raised when our command is about to execute. Here we set isExecuting to true 
        // And then raising our action which was stored previously and then we return isExecuting to false
        // So we can run this action again later
        // By default isExecuting is set to false
        public void Execute(object parameter)
        {
            if (isExecuting)
                return;

            isExecuting = true;
            executableAction();
            isExecuting = false;
        }
    }
}

