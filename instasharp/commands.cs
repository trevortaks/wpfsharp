using instasharp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
/*
 * Commands for handling various events in app
 */
namespace instasharp
{
    class Login : ICommand 
    {
        public ViewModel _view;

        public bool CanExecute(object Parameter) { 
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object Parameter) {
            var values = (object[])Parameter;

            _view = (ViewModel)values[0];
            var username = (string)values[1];
            var password = (PasswordBox)values[2];

            _view.SecurePassword = password.SecurePassword;
            _view.Login(username);
            
        }
    }

    class likeMedia : ICommand
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

            //_view.likeMedia(mediaID);
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
            _view.loadViews();
        }
    }

    class loadComments : ICommand 
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

            _view.selectedPopup = select;
            _view.popupShow = "Visible";
            _view.loadPostComments(mediaID);

        }
    }

    class loadLikers : ICommand
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

            _view.loadPostLikers(mediaID);
            _view.selectedPopup = select;
            _view.popupShow = "Visible";
        }
    }

    class loadFollowers : ICommand 
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
            string username = (string)values[1];
            //_view = new CommentsVM();
           _view.loadUserFollowers(username);
            
            _view.popupShow = "Visible";
            _view.selectedPopup = 0;

            
        } 
    }

    class loadFollowing : ICommand 
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
            string username = (string)values[1];

            _view.loadUserFollowing(username);
            _view.popupShow = "Visible";
            _view.selectedPopup = 0;
        }
    }

    class loadLogin : ICommand 
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
            _view.selectedPopup = 2;
        }
    }

    class loadProfile : ICommand 
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

            _view.selectedView = selectedOption;
            _view.loadUserDetails(username);
        }
    }

    class closePopup : ICommand
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
            if (_view.selectedPopup == 2)
            {
                _view.userLogin = 1;
            }
            //_view.loadFeed();
        }
    }

    class saveImage : ICommand 
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

           Task.Run(() => _view.getImage(url));
        }
    }
}
