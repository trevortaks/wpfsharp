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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
       List<Post> posts = new List<Post>();
       public ViewModel _model;

        public MainWindow()
        {

            InitializeComponent();
            _model = new ViewModel();

            DataContext = _model;

            //PopupWindow ppWindow= new PopupWindow(_model);
            //ppWindow.ShowDialog();

            //ppLogin.IsOpen = true;

            //if (!ppLogin.IsOpen) 
            //{
            //   // dpDock.Opacity = 0;
            //    var effect = new BlurEffect();
            //    var current = this.Background;
            //    effect.Radius = 5;
            //    //dpDock.Effect = effect;
            //    this.Background = new SolidColorBrush(Colors.DarkGray);
            //    this.Effect = effect;
            //    ppLogin.IsOpen = true;
            //}
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered by " + e.Source.ToString());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
