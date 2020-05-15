using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class LikeMedia : ICommand
    {
        public ViewModel _view;

        public bool CanExecute(object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object Parameter)
        {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            var mediaID = (string)values[1];

            //_view.likeMedia(mediaID);
        }
    }
}
