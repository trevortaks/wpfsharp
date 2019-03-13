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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        //User currentUser = new User("trevortaks", "tsitsiscoco");
        //public ViewModel _model;

        public MainView()
        {
            InitializeComponent();
            //_model = new ViewModel(currentUser);
            //DataContext = _model;
            //gdMain.DataContext = _model;

        }
    }
}
