using MediaElementKit;
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
    /// Interaction logic for FeedView.xaml
    /// </summary>
    public partial class FeedView : UserControl
    {
        bool isPlaying = false;

        public FeedView()
        {
            InitializeComponent();
        }


        private void meVideo_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MediaElementPro me = (MediaElementPro) sender;
            
            if (!isPlaying)
            {
                me.Play();
                isPlaying = true;
            }
            else 
            {
                me.Pause();
                isPlaying = false;
                
            }
        }

        private void btnLike_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;

            label.FontFamily = Application.Current.TryFindResource("FontAwesomeSolid") as FontFamily;
        }

        private void meVideo_Loaded_1(object sender, RoutedEventArgs e)
        {
            MediaElementPro me = (MediaElementPro)sender;
            me.Play();
            me.Pause();
        }
    }
}
