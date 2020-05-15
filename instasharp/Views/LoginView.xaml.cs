using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace instasharp.Views
{
    /// <summary>
    /// Interaction logic for ppLogin.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        //ViewModel _view;
        public LoginView()
        {
            InitializeComponent();
            //_view = (ViewModel)this.DataContext;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //if (this.DataContext != null) {
            //    //_view.SecurePassword = ((PasswordBox)sender).SecurePassword;
            //    _view = (ViewModel)this.DataContext;
            //    _view.SecurePassword = pwdPwd.SecurePassword;

            //}
        }

        //private void pwdPwd_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (this.DataContext != null) {
        //        _view.SecurePassword = ((PasswordBox)sender).SecurePassword;
        //    }
        //}

    }
}
