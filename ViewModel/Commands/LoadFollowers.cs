using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class LoadFollowers : ICommand
    {
        ViewModel _view;
        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(Object Parameter)
        {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            string username = (string)values[1];
            //_view = new CommentsVM();
            await _view.loadUserFollowers(username);

            _view.popupShow = "Visible";
            _view.SelectedPopup = 0;


        }
    }
}
