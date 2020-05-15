using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class ClosePopup : ICommand
    {
        ViewModel _view;
        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter)
        {
            var values = (object)Parameter;
            _view = (ViewModel)values;


            _view.popupShow = "Hidden";
            if (_view.SelectedPopup == 2)
            {
                _view.userLogin = 1;
            }
            //_view.loadFeed();
        }
    }
}
