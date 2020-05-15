using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModels;

namespace ViewModels.Commands
{
    public class LoadFollowing : ICommand
    {
        ViewModel _view;
        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter)
        {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            string username = (string)values[1];

            _view.loadUserFollowing(username);
            _view.popupShow = "Visible";
            _view.SelectedPopup = 0;
        }
    }
}
