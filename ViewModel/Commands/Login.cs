using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class Login : ICommand
    {
        public ViewModel _view;

        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter)
        {
            var values = (object[])Parameter;

            _view = (ViewModel)values[0];
            var username = (string)values[1];
            var password = (SecureString)values[2];

            _view.SecurePassword = password;
            _view.Login(username);

        }
    }
}
