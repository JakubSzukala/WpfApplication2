using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApplication2.ViewModels
{
    public class mButtonCommand : ICommand
    {
        // ICommand basically assigns a method to execute to a button for example

        // action is a delegate (smth like function pointer)
        // it encapsulates a method (here: to be executed after btn click)
        private readonly Action _handler;
        private bool _isEnabled;
        public mButtonCommand(Action handler)
        {
            _handler = handler;
            _isEnabled = true;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _isEnabled;
        }

        public void Execute(object parameter)
        {
            // execute function assigned to this action
            _handler();
        }


        public bool isEnabled
        {
            get { return isEnabled; }
            set 
            {
                // set value of main field and invoke the event
                _isEnabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
