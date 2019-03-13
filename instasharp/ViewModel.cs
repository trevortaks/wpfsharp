using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace instasharp
{
    public class ViewModel : INotifyPropertyChanged
    {
        User currentUser;
        string name = "Trevor Takawira";
        string imageSource = "/assets/image.jpg";

        public ICommand Like
        {
            get;
            private set;
        }

        private ICommand _likePicCommand;

        public ICommand likePic {
            get { return _likePicCommand; }
            set{
                _likePicCommand = value;
                OnPropertyChanged("likePic");
            }
        }

        private ICommand _collapseMenu;
        public ICommand collapseMenu {
            get { return _collapseMenu; }
            set {
                _collapseMenu = value;
                OnPropertyChanged("collapseMenu");
            }
        }

        private bool _showMenu = false;
        public bool showMenu
        {
            get { return _showMenu; }
            set {
                _showMenu = value;
                OnPropertyChanged("showMenu");
            }
        }

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts {
            get { return _posts; }
            set { _posts = value;
            OnPropertyChanged();
            }
        }
   
        public ViewModel(User user) {
            currentUser = user;
            //Task.Run(() => populateFeed()).GetAwaiter().GetResult();
            Task.Run(() => populateFeed()).ContinueWith((t) =>
            {
                
            },
            TaskScheduler.FromCurrentSynchronizationContext()
            );
            
            loadList();
            //post = new ObservableCollection<Post>();
            //post = posts;
             _likePicCommand = new Commands();
             _collapseMenu = new collapseMenus();
            
        }

        private void loadList(){
         
            _posts.Add(new Post()
            {
                likesCount =  300,
                caption = "This is a caption",
                commentsCount = "200",
                userName = "trevortals",
                url = "/assets/image.jpg",
                isImage = true,
                mediaID = "200"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image2.jpg",
                //url = "/assets/image2.jpg",
                isImage = true,
                mediaID = "100"
            });

            _posts.Add(new Post()
            {
                likesCount = 60,
                caption = "This is a caption 3",
                commentsCount = "20",
                userName = "trs",
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\video2.mp4",
                isImage = false,
                mediaID = "300"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                url = "/assets/video_image.jpg",
                //url = "/assets/image3.jpg",
                isImage = true,
                mediaID = "400"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                //url = "/assets/video_image.jpg"
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image4.jpg",
                isImage = true,
                mediaID = "500"
            });

          // MainWindow.icPost.ItemsSource = posts;
        }

        public async Task<bool> populateFeed()
        {
            
            var feed = await currentUser.getFeed();

            var b = 0;
            //List<Post> posts = new List<Post>();
            if (feed.Succeeded)
            {

                foreach (var media in feed.Value.Medias)
                {
                    var id = media.InstaIdentifier;
                    int likes = media.LikesCount;
                    string captionT = media.Caption.Text;
                    string comments = media.CommentsCount;
                    string name = media.User.UserName;
                    bool isLiked = media.HasLiked;
                    bool isImage = false;
                    if (media.MediaType.ToString() == "image") isImage = true;
                    List<string> imgURLs = new List<string>();
                    foreach (var item in media.Images)
                        imgURLs.Add(item.URI);
                    _posts.Add(new Post()
                    {
                        mediaID = id,
                        isLiked = isLiked,
                        likesCount = likes,
                        caption = captionT,
                        commentsCount = comments,
                        userName = name,
                        isImage = isImage
                    });

                    var a = 0;
                    
                }
                return true;
            }
            return false; 
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //private object mediaID;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void likeMedia(string mediaID)
        {
            var result = Task.Run(() => currentUser.likePost(mediaID)).GetAwaiter().GetResult();
            //if(result)
        }

       


        /*internal void likeMedia(string mediaID)
        {
            throw new NotImplementedException();
        }*/
    }

    public class Post
    {
        public string mediaID { get; set; }
        public int likesCount { get; set; }
        public string caption { get; set; }
        public string commentsCount { get; set; }
        public string userName { get; set; }
        public string url { get; set; }
        public bool isImage { get; set; }
        public bool isLiked { get; set; }
    }

    public class joinConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    
    }
}
