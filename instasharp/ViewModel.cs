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
using instasharp.Models;
using MediaElementKit;
using System.Drawing;
using System.Net;

namespace instasharp
{
    public class ViewModel : INotifyPropertyChanged 
    {
        protected User _currentUser;
        
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

        private ICommand _loadFollower;
        public ICommand loadFollower
        {
            get { return _loadFollower; }
            set
            {
                _loadFollower = value;
                OnPropertyChanged("loadFollower");
            }
        }

        private ICommand _loadFollowee;
        public ICommand loadFollowee
        {
            get { return _loadFollowee; }
            set
            {
                _loadFollowee = value;
                OnPropertyChanged("loadFollowee");
            }
        }

        private ICommand _loadLogin;
        public ICommand loadLogin
        {
            get { return _loadLogin; }
            set
            {
                _loadLogin = value;
                OnPropertyChanged("loadLogin");
            }
        }

        private ICommand _loadDetails;
        public ICommand loadDetails
        {
            get { return _loadDetails; }
            set
            {
                _loadDetails = value;
                OnPropertyChanged("loadLogin");
            }
        }

        private ICommand _saveMedia;
        public ICommand saveMedia
        {
            get { return _saveMedia; }
            set
            {
                _saveMedia = value;
                OnPropertyChanged("saveMedia");
            }
        }

        private ObservableCollection<Post> _feedPosts = new ObservableCollection<Post>();
        public ObservableCollection<Post> feedPosts
        {
            get { return _feedPosts; }
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

        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("profilePicUrl");
            }
        }

        private string _profilePicUrl;
        public string profilePicUrl
        {
            get { return _profilePicUrl; }
            set {
                _profilePicUrl = value;
                OnPropertyChanged("profilePicUrl");
            }
        }

        private int _userLogin = 1;
        public int userLogin 
        {
            get { return _userLogin; }
            set {
                _userLogin = value;
                OnPropertyChanged("userLogin");
            }
        }

        public SecureString SecurePassword
        {
            get;
            set;
        }

        private string _errorMessage;
        public string errorMessage 
        {
            get { return _errorMessage; }
            set{
                _errorMessage = value;
                OnPropertyChanged("errorMessage");
            }
        }

        public ViewModel() {
             _likePicCommand = new likeMedia();
             _collapseMenu = new collapseMenus();
             _changeView = new changeViews();
             _loadComment = new loadComments();
             _loadLiker = new loadLikers();
             _closePopup = new closePopup();
             _login = new Login();
             _loadFollower = new loadFollowers();
             _loadFollowee = new loadFollowing();
             _loadLogin = new loadLogin();
             _saveMedia = new saveImage();
             _loadDetails = new loadProfile();

             const string stateFile = "state.bin";

             if (File.Exists(stateFile))
             {
                 try
                 {
                     _currentUser = new User();
                     if (_currentUser.login)
                     {
                         userLogin = 2;
                         popupShow = "Hidden";
                         userName = _currentUser.currentUser.Value.UserName;
                         profilePicUrl = _currentUser.currentUser.Value.ProfilePicUrl;
                         loadFeed();
                     }
                 }
                 catch (System.NullReferenceException e) {
                     //System.Windows.MessageBox.Show("Error in Sending Request: Check your connection and try again");
                     userLogin = 3;
                 }

                 
             }

             //MediaElementPro me = new MediaElementPro();

        }

        public void Login(string username){
            
            _currentUser = new User(username, SecurePassword);
            
            if (_currentUser.login) 
            {
                userLogin = 2;
                popupShow = "Hidden";
                userName = _currentUser.currentUser.Value.UserName;
                profilePicUrl = _currentUser.currentUser.Value.ProfilePicUrl;
                loadFeed();
            }
            else
            {
                errorMessage = _currentUser.loginResult;
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

        private void loadFeed()
        {
            //App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFeed(); });
            loadList();
        }


        private void loadMessages()
        {
            throw new NotImplementedException();
        }

        private void loadExplore()
        {
            //populateExploreFeed();
            loadList();
        }

        private void loadProfile()   
        {
            throw new NotImplementedException();
        }


        public void loadPostComments(string mediaID) {
            //App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateComments(mediaID); });
            ppComments();
        }

        public void loadFollowActivity() {
            //App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateFollowActivity(); });
            ppFollowActivity();
        }

        public void loadPostLikers(string mediaID) {
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateLikers(mediaID); });
            //ppLikers();
        }

        public void loadUserDetails(){
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateUserDetails("trevortaks"); }); 
            //ppUserDetails();
        }

        public void loadUserDetails(string uname)
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate() { populateUserDetails(uname); });
            //ppUserDetails();
        }

        public void loadUserFollowers(string uname) {
            populateUserFollowers(uname);
            //ppLikers();
        }

        public void loadUserFollowing(string uname) {
            populateUserFollowing(uname);
            //ppLikers();
        }

        internal void populateUserDetails(string username) 
        {
            var user =  User.getUserDetails(username);
            if (user.IsCompleted)
            {
                _userDetails.userName = user.Result.Value.Username;
                _userDetails.fullName = user.Result.Value.FullName;
                _userDetails.followers = user.Result.Value.FollowerCount.ToString() + "\n";
                _userDetails.following = user.Result.Value.FollowingCount.ToString() + "\n";
                _userDetails.profilePic = user.Result.Value.ProfilePicUrl;
                _userDetails.isFollowing = user.Result.Value.FriendshipStatus.Following;
                _userDetails.postsCount = user.Result.Value.MediaCount.ToString() + "\n";
                _userDetails.biography = user.Result.Value.Biography;
            }

            //var posts = await _currentUser.getUserPosts(username);
            //if (posts.Succeeded && posts.Value.Count > 0)
            //{
            //    foreach (var post in posts.Value)
            //    {
            //        try
            //        {
            //            _userDetails.posts.Add(
            //                    new Post()
            //                    {
            //                        url = post.Images[0].Uri
            //                    }
            //                );
            //        }
            //        catch (System.NullReferenceException)
            //        {
            //            continue;
            //        }
            //        catch (Exception e)
            //        {
            //            continue;
            //        }
            //    }
            //}
        }

        private async Task populateFeed()
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
            }
            else 
            {
                var error = feed.Info.Message;
            }
        }

        private async Task populateComments(string mediaID)
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

        private async Task populateLikers(string mediaID)
        {
            _likers.Clear();
            var likersList = await User.getLikers(mediaID);

            foreach (var liker in likersList.Value)
            {
                _likers.Add(liker.UserName);
            }
        }

        private async Task populateFollowActivity(){

            var activities = await _currentUser.getUserFollowingActivity();

            foreach (var activity in activities.Value.Items) {
                _likeActivity.Add(activity.Text);
            }
        }

        

        internal void populateUserFollowers(string username)
        {
            var followers = User.getUserFollowers(username);

            likers.Clear();
            foreach (var follower in followers.Result.Value)
            {
                likers.Add(follower.UserName);
            }
        }

        internal void populateUserFollowing(string username)
        {
            var following =  User.getUserFollowing(username);
            likers.Clear();
            foreach (var followed in following.Result.Value)
            {
                likers.Add(followed.UserName);
            }
        }

        internal void populateExploreFeed() 
        {
            var feed = User.getExploreFeed();
            foreach (var item in feed.Result.Value.Medias) {
                feedPosts.Add(
                    new Post
                    {
                       caption  = item.Caption.Text,
                       url = item.Images[0].Uri,
                       likesCount = item.LikesCount,
                       mediaID = item.InstaIdentifier
                    }
                    );
            }
        }

        public void likeMedia(string mediaID)
        {
            var result = Task.Run(() => _currentUser.likePost(mediaID)).GetAwaiter().GetResult();
        }

        public async Task getImage(string url)
        {
            Image image = await downloadMedia(url);

            string rootpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string filepath = System.IO.Path.Combine(rootpath + @"\Pictures\Saved Pictures\image.jpg");

            image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        public Task<Image> downloadMedia(string url) 
        {
            Image image = null;

            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.AllowWriteStreamBuffering = true;
                webrequest.Timeout = 50000;

                WebResponse webresponse = webrequest.GetResponse();
                System.IO.Stream stream =  webresponse.GetResponseStream();

                image = Image.FromStream(stream);

                webresponse.Close();
            }
            catch (Exception e) 
            {
                return null;
            }

            return Task.Run(() => { return image; });
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
                //url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image2.jpg",
                url = "/assets/image2.jpg",
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
                url = "/assets/image9.bmp",
                isImage = true,
                mediaID = "300"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                isLiked = false,
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\video2.mp4",
                isImage = false,
                mediaID = "400"
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                isLiked = true,
                url = @"/assets/image8.jpg",
                isImage = true,
                mediaID = "500"
            });
            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                isLiked = true,
                url = "/assets/image4.jpg",
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

        private void ppUserDetails()
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

        private void ppComments()
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

        private void ppLikers()
        {
            likers.Clear();
            for (int i = 0; i < 20; i++)
            {
                _likers.Add("user" + i);

            }
        }    
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
