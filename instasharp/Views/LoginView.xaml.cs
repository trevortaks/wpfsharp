using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for ppLogin.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        ViewModel _view;
        public LoginView()
        {
            InitializeComponent();
           // _view = (ViewModel) this.DataContext;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null) {
                //_view.SecurePassword = ((PasswordBox)sender).SecurePassword;
                _view = (ViewModel)this.DataContext;
                _view.SecurePassword = pwdPwd.SecurePassword;
               
            }
        }

        //private void pwdPwd_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    if (this.DataContext != null) {
        //        _view.SecurePassword = ((PasswordBox)sender).SecurePassword;
        //    }
        //}

    }
}
