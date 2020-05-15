using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class CollapseMenus : ICommand
    {
        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter)
        {
            ViewModel _view = (ViewModel)Parameter;
            _view.ShowMenu = !_view.ShowMenu;
        }
    }
}
