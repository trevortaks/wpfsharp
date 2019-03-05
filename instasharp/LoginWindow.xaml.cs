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
using System.Windows.Shapes;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(txtUsername.Text, txtPassword.Password.ToString());

            if (user.loginResult == "Success")
            {
                lblStatus.Content = user.loginResult;
                ViewModel model = new ViewModel(user);
                MainWindow window = new MainWindow(model);
                window.Show();
                this.Close();
            }
            else {
                lblStatus.Content = user.loginResult;
            }

        }
    }
}
