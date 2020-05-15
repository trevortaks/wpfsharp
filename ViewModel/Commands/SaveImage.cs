using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class SaveImage : ICommand
    {
        ViewModel _view;
        public bool CanExecute(Object Parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter)
        {
            var values = (object[])Parameter;
            _view = (ViewModel)values[0];
            string url = (string)values[1];

            Task.Run(() => _view.GetImage(url));
        }
    }
}
