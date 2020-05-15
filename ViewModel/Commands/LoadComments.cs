using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class LoadComments : ICommand
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
            var mediaID = (string)values[1];
            var select = Convert.ToInt32(values[2]);

            _view.SelectedPopup = select;
            _view.popupShow = "Visible";
            _view.loadPostComments(mediaID);

        }
    }
}
