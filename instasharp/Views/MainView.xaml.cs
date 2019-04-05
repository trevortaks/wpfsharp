using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Vlc.DotNet.Wpf;

namespace instasharp
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        
        public MainView()
        {
            InitializeComponent();

           // var currentAssembly = Assembly.GetEntryAssembly();
           // var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
           // //VlcControl vlc = new VlcControl();
           // var vlcLibDirectory = new DirectoryInfo(
           //         System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86":"win-x64")
           //     );
           // var options = new string[] {
                
           // };

           //icPost.

           // MyControl.SourceProvider.CreatePlayer(vlcLibDirectory, options);
        }
    }
}
