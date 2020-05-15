using System.Windows;
using ViewModels;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public ViewModel _model;

        public MainWindow()
        {

            InitializeComponent();
            _model = new ViewModel();

            DataContext = _model;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            _model.ClosePopup.Execute(_model);

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
    }
}

