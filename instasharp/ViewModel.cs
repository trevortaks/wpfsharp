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

        string _name;
        public string name
        {
            get;
            private set;
        }

        string _commentsNum;
        public string commentsNum
        {
            get;
            private set;
        }
        
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

        private int _selectedPopup = 1;
        public int selectedPopup
        {
            get { return _selectedPopup; }
            set {
                _selectedPopup = value;
                OnPropertyChanged("selectedPopup");
            }
        }

        private ICommand _loadComment;
        public ICommand loadComment 
        {
            get { return _loadComment; }
            set {
                _loadComment = value;
                OnPropertyChanged("loadComments");
            }
        }

        private ICommand _loadLiker;
        public ICommand loadLiker 
        {
            get { return _loadLiker; }
            set {
                _loadLiker = value;
                OnPropertyChanged("loadLikers");
            }
        }

        private ICommand _closePopup;
        public ICommand closePopup {
            get { return _closePopup; }
            set {
                _closePopup = value;
                OnPropertyChanged("closePopup");
            }
        }

        private ObservableCollection<string> _likers = new ObservableCollection<string>();
        public ObservableCollection<string> likers
        {
            get { return _likers; }
        }

        private ObservableCollection<Comment> _comments = new ObservableCollection<Comment>();
        public ObservableCollection<Comment> comments
        {
            get { return _comments; }
        }

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts {
            get { return _posts; }
        }

        private bool _popupShow = false;
        public bool popupShow
        {   get { return _popupShow; }
            set {
                _popupShow = value;
                OnPropertyChanged("popupShow");
            }
        }

        public ViewModel() {
            name = "trevortaks";
            commentsNum = String.Format("11 \n");
            currentUser = new User(name, "");
            loadFeed();
            
             _likePicCommand = new Commands();
             _collapseMenu = new collapseMenus();
             _changeView = new changeViews();
             _loadComment = new loadComments();
             _loadLiker = new loadLikers();
             _closePopup = new closePopup();
            
        }

        private void loadFeed(){
           if (currentUser.login)
           {
               App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFeed(); });
               loadList();
           }
           else
           {
               loadList();
           }
       }

        public void loadPostComments(string mediaID) {
            //App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateComments(mediaID); });
            ppComments();
        }
        

        public async Task populateFeed()
        {
            
            var feed = await currentUser.getFeed();

            if (feed.Succeeded)
            {

                //foreach (var media in feed.Value.Medias)
                //{
                    for (int i = 0; i < 20; i++)
                    {
                        var media = feed.Value.Medias[i];
                        bool isImage = false;
                        string url = "";
                        if (media.MediaType.ToString() == "Image")
                        {
                            isImage = true;
                           // if (media.Images.Count > 0) url = media.Images[0].URI;
                        }
                        if (media.MediaType.ToString() == "Video")
                        {
                            //isImage = true;
                            if (media.Videos.Count > 0) url = media.Videos[0].Url;
                        }
                        try
                        {
                            _posts.Add(new Post()
	                            {
	                                mediaID = media.InstaIdentifier,
	                                isLiked = media.HasLiked,
	                                likesCount = media.LikesCount,
	                                caption = media.Caption.Text,
	                                commentsCount = media.CommentsCount,
	                                userName = media.User.UserName,
	                                isImage = isImage,
	                                date = media.TakenAt
	                                //url = url

	                            }
                            );
                        }
                        catch (System.NullReferenceException)
                        {
                            continue;
                        }
                  }
                //}
            }
            else 
            {
                var error = feed.Info.Message;
            }
        }

        public async Task populateComments(string mediaID) 
        {
            _comments.Clear();
            var commentsList = await currentUser.getComments(mediaID);

            foreach (var comment in commentsList.Value.Comments)
            {
                _comments.Add(
                    new Comment
                    {
                        userName = comment.User.UserName,
                        comment = comment.Text
                    }
                    );
            }
        }

        public void ppComments() {
            for (int i = 0; i < 20; i++)
            {
                _comments.Add(new Comment
                {
                    userName = "user" + i,
                    comment = "This is a comment"
                });

            }
        }

        public void ppLikers()
        {
            for (int i = 0; i < 20; i++)
            {
                _likers.Add("user" + i);

            }
        }

        public async Task populateLikers(string mediaID) 
        {
            _likers.Clear();
            var likersList = await currentUser.getLikers(mediaID);

            foreach (var liker in likersList.Value) 
            {
                _likers.Add(liker.UserName);
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

        private void loadList()
        {

            _posts.Add(new Post()
            {
                likesCount = 300,
                caption = "This is a caption",
                commentsCount = "200",
                isLiked = true,
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
                isLiked = false,
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
                isLiked = true,
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
                isLiked = false,
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
                isLiked = true,
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image4.jpg",
                isImage = true,
                mediaID = "500"
            });
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

    public class Comment 
    {
        public string userName { get; set; }
        public string comment { get; set; }
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
