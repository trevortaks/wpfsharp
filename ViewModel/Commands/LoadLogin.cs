using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class LoadLogin : ICommand
    {
        ViewModel _view;
        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter)
        {
            var values = (object)Parameter;
            _view = (ViewModel)values;

            _view.popupShow = "Visible";
            _view.SelectedPopup = 2;
        }
    }
}
