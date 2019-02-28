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
