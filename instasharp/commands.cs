using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace instasharp
{
    class Commands : ICommand
    {
        public ViewModel _view;

        public bool CanExecute( object Parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter) {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            var mediaID = (string)values[1];

            _view.likeMedia(mediaID);
        }
    }

    class collapseMenus : ICommand
    {
        public bool CanExecute(object Parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter) {
            ViewModel _view = (ViewModel)Parameter;
            _view.showMenu = !_view.showMenu;
            var a = 0;
        }
    }

    class changeViews : ICommand 
    {
        ViewModel _view;
        public bool CanExecute(object Parameter) 
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter) {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            var selectedOptio = values[1].ToString();
            var selectedOption = Convert.ToInt32(selectedOptio);

            _view.selectedView = selectedOption;
        }
    }

    class PlayCommand : ICommand {
        public ViewModel _view;
        public bool CanExecute(object parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter)
        {
            
            
        }
    }
}
