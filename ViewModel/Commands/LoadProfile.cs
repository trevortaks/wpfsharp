using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class LoadProfile : ICommand
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
            var selectedOptio = values[1].ToString();
            var selectedOption = Convert.ToInt32(selectedOptio);
            var username = values[2].ToString();

            _view.SelectedView = selectedOption;
            _view.loadUserDetails(username);
        }
    }
}
