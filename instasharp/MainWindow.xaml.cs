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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        User currentUser = new User("trevortaks", "tsitsiscoco");
       List<Post> posts = new List<Post>();
       public ViewModel _model;

        public bool VideoIsPlaying = false;

        public MainWindow()
        {

            InitializeComponent();
            _model = new ViewModel(currentUser);
            //DataContext = _model;
            gridHome.DataContext = _model;
            
            // Create the binding.
            CommandBinding binding = new CommandBinding(MediaCommands.Play);
            // Attach the event handler.
            binding.Executed += Play_Executed;
            // Register the binding.
            this.CommandBindings.Add(binding);
            
           
           /* Task.Run(() => populateFeed()).ContinueWith((t) => {
                icPost.ItemsSource = posts;
                
            }, 
            TaskScheduler.FromCurrentSynchronizationContext()
            );*/

            //var a = 1;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered by " + e.Source.ToString());
        }

        private void spProfile_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        //public async Task populateFeed()
        //{

        //    var feed = await currentUser.getFeed();
        //    //List<Post> posts = new List<Post>();
        //    if (feed.Succeeded)
        //    {

        //        foreach (var media in feed.Value.Medias)
        //        {
        //            int likes = media.LikesCount;
        //            string captionT = media.Caption.Text;
        //            string comments = media.CommentsCount;
        //            string name = media.User.UserName;
        //            bool isImage = false;
        //            if (media.MediaType.ToString() == "image") isImage = true;
        //            List<string> imgURLs = new List<string>();
        //            foreach (var item in media.Images)
        //                imgURLs.Add(item.URI);
        //            posts.Add(new Post()
        //            {
        //                likesCount = likes,
        //                caption = captionT,
        //                commentsCount = comments,
        //                userName = name,
        //                isImage = isImage
        //            });

        //            var a = 0;

        //        }
        
        //    }

        //}

       // private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e) { 
       //     e.CanExecute = (meVideo != null)
        //}
        //public void New
    }
}
