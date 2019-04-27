using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace instasharp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        
        /*
         * Responsible for 
         */
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

        private int _selectedPopup = 2;
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

        private ICommand _login;
        public ICommand login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("login");
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

        private ObservableCollection<string> _likeActivity = new ObservableCollection<string>();
        public ObservableCollection<string>  likeActivity 
        {
            get { return _likeActivity; }
        }

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts {
            get { return _posts; }
        }

        private UserDetails _userDetails = new UserDetails();
        public UserDetails userDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged("userDetails");
            }
        }

        private string _popupShow = "Visible";
        public string popupShow
        {   get { return _popupShow; }
            set {
                _popupShow = value;
                OnPropertyChanged("popupShow");
            }
        }

        public string username
        {
            get;
            set;
        }

        public SecureString SecurePassword
        {
            get;
            set;
        }

        public ViewModel() {
             _likePicCommand = new likeMedia();
             _collapseMenu = new collapseMenus();
             _changeView = new changeViews();
             _loadComment = new loadComments();
             _loadLiker = new loadLikers();
             _closePopup = new closePopup();
             _login = new Login();
        }

        public void Login(string username){
            _currentUser = new User(username, SecurePassword.ToString());
            if (_currentUser.login) 
            {
                popupShow = "Hidden";
                loadFeed();  
            }
            
        }

        private void loadFeed(){
           if (_currentUser.login)
           {
               App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFeed(); });
               //loadList();
               //loadUserDetails();
               //loadFollowActivity();
           }
           else
           {
               loadList();
               //loadFollowActivity();
           }
       }

        public void loadViews() 
        {
            switch (_selectedView) 
            {
                case 1:
                    loadFeed();
                    break;
                case 2:
                    loadUserDetails();
                    break;
                case 3:
                    loadFollowActivity();
                    break;
                case 4:
                    loadExplore();
                    break;
                case 5:
                    loadMessages();
                    break;
            }
        }

        private void loadMessages()
        {
            throw new NotImplementedException();
        }

        private void loadExplore()
        {
            throw new NotImplementedException();
        }

        private void loadProfile()
        {
            throw new NotImplementedException();
        }

        public void loadPostComments(string mediaID) {
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateComments(mediaID); });
            //ppComments();
        }

        public void loadFollowActivity() {
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFollowActivity(); });
            //ppFollowActivity();
        }

        public void loadPostLikers(string mediaID) {
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateLikers(mediaID); });
            //ppLikers();
        }

        public void loadUserDetails(){
            //App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateUserDetails("trevortaks"); }); 
            ppUserDetails();
        }

        public async Task populateUserDetails(string username) 
        {
            var user = await _currentUser.getUserDetails(username);

                _userDetails.userName = user.Value.Username;
                _userDetails.fullName = user.Value.FullName;
                _userDetails.followers = user.Value.FollowerCount.ToString() + "\n";
                _userDetails.following = user.Value.FollowingCount.ToString() + "\n";
                _userDetails.profilePic = user.Value.ProfilePicUrl;
                _userDetails.isFollowing = user.Value.FriendshipStatus.Following;
                _userDetails.postsCount = user.Value.MediaCount.ToString() + "\n";
                _userDetails.biography = user.Value.Biography;


            var posts = await _currentUser.getUserPosts(username);
            if (posts.Succeeded && posts.Value.Count > 0)
            {
                foreach (var post in posts.Value)
                {
                    try
                    {
                        _userDetails.posts.Add(
                                new Post()
                                {
                                    url = post.Images[0].Uri
                                }
                            );
                    }
                    catch (System.NullReferenceException)
                    {
                        continue;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
        }

        public async Task populateFeed()
        {
            
            var feed = await _currentUser.getFeed();

            if (feed.Succeeded)
            {

                //foreach (var media in feed.Value.Medias)
                //{
                for (int i = 0; i < 7; i++)
                {
                        var media = feed.Value.Medias[i];
                        bool isImage = false;
                        string url = "";
                        if (media.MediaType.ToString() == "Image")
                        {
                            isImage = true;
                            if (media.Images.Count > 0) url = media.Images[0].Uri;
                        }
                        if (media.MediaType.ToString() == "Video")
                        {
                            //isImage = true;
                            if (media.Videos.Count > 0) url = media.Videos[0].Uri;
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
	                                date = media.TakenAt,
                                    userPic = media.User.ProfilePicture,
	                                url = url

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
            var commentsList = await _currentUser.getComments(mediaID);

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

        public async Task populateLikers(string mediaID)
        {
            _likers.Clear();
            var likersList = await _currentUser.getLikers(mediaID);

            foreach (var liker in likersList.Value)
            {
                _likers.Add(liker.UserName);
            }
        }

        public async Task populateFollowActivity(){

            var activities = await _currentUser.getUserFollowingActivity();

            foreach (var activity in activities.Value.Items) {
                string act = activity.Text;
                _likeActivity.Add(act);
            }
        }

        public void likeMedia(string mediaID)
        {
            var result = Task.Run(() => _currentUser.likePost(mediaID)).GetAwaiter().GetResult();

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

        public void ppFollowActivity()
        {
            
            _likeActivity.Add("User liked user");
            _likeActivity.Add("Trevor started following 5 other people");
            _likeActivity.Add("This guy liked the other guy's post");
            _likeActivity.Add("The dude posted for the first time in a while");
            _likeActivity.Add("These five guys liked the same pic");
            _likeActivity.Add("There reallu isnt much to add");
            _likeActivity.Add("Thats all the activity I can think of");
           
        }

        public void ppUserDetails()
        {
            _userDetails.userName = "trevortakawira";
            _userDetails.fullName = "Trevor Takawira";
            _userDetails.followers = Convert.ToString(160) + "\n";
            _userDetails.following = Convert.ToString(10357) + "\n";
            _userDetails.postsCount = Convert.ToString(100) + "\n";
            _userDetails.profilePic = "/assets/image.jpg";
            _userDetails.isFollowing = true;

            _userDetails.posts.Add(new Post()
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

            _userDetails.posts.Add(new Post()
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
        }

        public void ppComments()
        {
            _comments.Clear();
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
            likers.Clear();
            for (int i = 0; i < 20; i++)
            {
                _likers.Add("user" + i);

            }
        }


        internal void loadUserFollowers(string username)
        {
            var followers = _currentUser.getUserFollowers(username);

            likers.Clear();
            foreach (var follower in followers.Result.Value) 
            {
                likers.Add(follower.UserName);
            }
        }

        internal void loadUserFollowing(string username) 
        {
            var following = _currentUser.getUserFollowing(username);
            likers.Clear();
            foreach (var followed in following.Result.Value)
            {
                likers.Add(followed.UserName);
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
        public string userPic { get; set; }
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

    public class UserDetails 
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string profilePic { get; set; }
        public string followers { get; set; }
        public string following { get; set; }
        public string postsCount { get; set; }
        public bool isFollowing { get; set; }
        public string biography { get; set; }
        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts { get { return _posts; } }

        
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
