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
using System.Windows.Threading;

namespace instasharp
{
    public class ViewModel : INotifyPropertyChanged
    {
        User currentUser;
        /*
         * 
         */
        string _name = "trevortaks";
        public string name {
            get { return _name;}
            private set 
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }
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

        private ICommand _changeView;
        public ICommand changeView
        {
            get { return _changeView; }
            set {
                _changeView = value;
                OnPropertyChanged("changeView");
            }
        }

        private int _selectedView = 1;
        public int selectedView
        {
            get { return _selectedView; }
            set {
                _selectedView = value;
                OnPropertyChanged("selectedView");
            }
        }

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts {
            get { return _posts; }
            //set { _posts = value;
            //OnPropertyChanged("posts");
            //}
        }
   
        public ViewModel() {
            currentUser = new User("", "");
            //if(currentUser.)
            //Task.Run(() => populateFeed()).ContinueWith((t) =>
            //{
            //    loadList();
            //},
            //TaskScheduler.FromCurrentSynchronizationContext()
            //);
            loadFeed();
             _likePicCommand = new Commands();
             _collapseMenu = new collapseMenus();
             _changeView = new changeViews();
            
        }

       private void loadFeed(){
           if (currentUser.login)
           {
               App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFeed(); });
           }
           else
           {
               loadList();
           }
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
                isImage = true,
                mediaID = "400"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image4.jpg",
                isImage = true,
                mediaID = "500"
            });
        }

        public async Task populateFeed()
        {
            
            var feed = await currentUser.getFeed();

            if (feed.Succeeded)
            {

                foreach (var media in feed.Value.Medias)
                {
                    bool isImage = true;
                    if (media.MediaType.ToString() == "Video") isImage = false;
                    try
                    {
                        var one = new Post()
                        {
                            mediaID = media.InstaIdentifier,
                            isLiked = media.HasLiked,
                            likesCount = media.LikesCount,
                            caption = media.Caption.Text,
                            commentsCount = media.CommentsCount,
                            userName = media.User.UserName,
                            isImage = isImage,
                            date = media.TakenAt

                        };
                        _posts.Add(one);
                    }
                    catch (System.NullReferenceException)
                    {
                        continue;
                    }
                }
            }
            else 
            {
                var error = feed.Info.Message;
            }
        }

        public void likeMedia(string mediaID)
        {
            var result = Task.Run(() => currentUser.likePost(mediaID)).GetAwaiter().GetResult();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
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
        public DateTime date { get; set; }
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
