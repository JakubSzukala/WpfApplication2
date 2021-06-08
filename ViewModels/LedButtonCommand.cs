using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApplication2.ViewModels
{
    public class LedButtonCommand : ICommand
    {
        private readonly Action _handler;
        private int _posX;
        private int _posY;
        private bool _isEnabled;

        public LedButtonCommand(Action handler, int posX, int posY)
        {
            _handler = handler;
            _posX = posX;
            _posY = posY;
            _isEnabled = true;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _isEnabled;
        }

        public void Execute(object parameter)
        {
            _handler();
        }
        public bool isEnabled
        {
            get { return _isEnabled; }
            set
            {
                // set value of main field and invoke the event
                _isEnabled = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
