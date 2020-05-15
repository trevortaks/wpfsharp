using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using ViewModels.Commands;
using Logic;
using Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Threading;
using ViewModels.Constants;

namespace ViewModels  
{
    /*
     * ViewModel class provides data to the application 
     * Should be separated into different views at later stage
     */
    public class ViewModel : INotifyPropertyChanged 
    {

        protected User _currentUser;

        //Command for liking a post
        private ICommand _likePicCommand;
        public ICommand LikePic {
            get { return _likePicCommand; }
            set{
                _likePicCommand = value;
                OnPropertyChanged(nameof(LikePic));
            }
        }

        //Command to collapse the sidebar menu
        private ICommand _collapseMenu;
        public ICommand CollapseMenu {
            get { return _collapseMenu; }
            set {
                _collapseMenu = value;
                OnPropertyChanged(nameof(CollapseMenu));
            }
        }
        
        //Command to show the sidebar menu
        private bool _showMenu = false;
        public bool ShowMenu
        {
            get { return _showMenu; }
            set {
                _showMenu = value;
                OnPropertyChanged(nameof(ShowMenu));
            }
        }

        //Command to toggle between different views
        private ICommand _changeView;
        public ICommand ChangeView
        {
            get { return _changeView; }
            set {
                _changeView = value;
                OnPropertyChanged(nameof(ChangeView));
            }
        }

        //Int pointing to currently selected view
        private int _selectedView = 1;
        public int SelectedView
        {
            get { return _selectedView; }
            set {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        //int pointing to currently selected view in childwindow modal
        private int _selectedPopup = 2;
        public int SelectedPopup
        {
            get { return _selectedPopup; }
            set {
                _selectedPopup = value;
                OnPropertyChanged(nameof(SelectedPopup));
            }
        }
        
        //Command to initiate function that loads comments for particular post
        private ICommand _loadComment;
        public ICommand LoadComment 
        {
            get { return _loadComment; }
            set {
                _loadComment = value;
                OnPropertyChanged("loadComments");
            }
        }

        /// <summary>
        /// Command to load list of likers for a post 
        /// </summary>
        private ICommand _loadLiker;
        public ICommand LoadLiker 
        {
            get { return _loadLiker; }
            set {
                _loadLiker = value;
                OnPropertyChanged("loadLikers");
            }
        }

        /// <summary>
        /// Command to close the childwindow popup
        /// </summary>
        private ICommand _closePopup;
        public ICommand ClosePopup {
            get { return _closePopup; }
            set {
                _closePopup = value;
                OnPropertyChanged("closePopup");
            }
        }

        /// <summary>
        /// Command to initiate login procedure in homepage
        /// </summary>
        private ICommand _login;
        public ICommand LoginAction
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("login");
            }
        }

        /// <summary>
        /// Command to load followers for current selected profile
        /// </summary>
        private ICommand _loadFollower;
        public ICommand LoadFollower
        {
            get { return _loadFollower; }
            set
            {
                _loadFollower = value;
                OnPropertyChanged("loadFollower");
            }
        }

        /// <summary>
        /// Command to load followers of current selected profile
        /// </summary>
        private ICommand _loadFollowee;
        public ICommand LoadFollowee
        {
            get { return _loadFollowee; }
            set
            {
                _loadFollowee = value;
                OnPropertyChanged("loadFollowee");
            }
        }

        /// <summary>
        /// Command to show the login window in child window modal
        /// </summary>
        private ICommand _loadLogin;
        public ICommand LoadLogin
        {
            get { return _loadLogin; }
            set
            {
                _loadLogin = value;
                OnPropertyChanged("loadLogin");
            }
        }

        private ICommand _loadDetails;
        public ICommand LoadDetails
        {
            get { return _loadDetails; }
            set
            {
                _loadDetails = value;
                OnPropertyChanged("loadDetails");
            }
        }

        /// <summary>
        /// Command to save media from current post
        /// </summary>
        private ICommand _saveMedia;
        public ICommand SaveMedia
        {
            get { return _saveMedia; }
            set
            {
                _saveMedia = value;
                OnPropertyChanged("saveMedia");
            }
        }

        public ObservableCollection<Post> FeedPosts { get; } = new ObservableCollection<Post>();
        public ObservableCollection<string> Likers { get; } = new ObservableCollection<string>();
        public ObservableCollection<Comment> Comments { get; } = new ObservableCollection<Comment>();
        public ObservableCollection<string> LikeActivity { get; } = new ObservableCollection<string>();
        public ObservableCollection<Post> Posts { get; } = new ObservableCollection<Post>();

        /// <summary>
        /// Variable for holding the details of current logged in user
        /// </summary>
        private UserDetails _userDetails = new UserDetails();
        public UserDetails UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                OnPropertyChanged("userDetails");
            }
        }

        /// <summary>
        /// Toggle for the child window modal
        /// Bound to the visibilty property of childwindow
        /// </summary>
        private string _popupShow = "Visible";
        public string popupShow
        {   get { return _popupShow; }
            set {
                _popupShow = value;
                OnPropertyChanged("popupShow");
            }
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("userName");
            }
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        private string _profilePicUrl;
        public string profilePicUrl
        {
            get { return _profilePicUrl; }
            set {
                _profilePicUrl = value;
                OnPropertyChanged("profilePicUrl");
            }
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        private int _userLogin = (int)UserLogin.Loggedout;
        public int userLogin 
        {
            get { return _userLogin; }
            set {
                _userLogin = value;
                OnPropertyChanged("userLogin");
            }
        }

        /// <summary>
        /// Self explanatory
        /// </summary>
        public SecureString SecurePassword
        {
            get;
            set;
        }

        /// <summary>
        /// String containing the error message 
        /// </summary>
        private string _errorMessage;
        public string errorMessage 
        {
            get { return _errorMessage; }
            set{
                _errorMessage = value;
                OnPropertyChanged("errorMessage");
            }
        }

        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        public ViewModel()
        {

            _likePicCommand = new LikeMedia();
            _collapseMenu = new CollapseMenus();
            _changeView = new ChangeViews();
            _loadComment = new LoadComments();
            _loadLiker = new LoadLikers();
            _closePopup = new ClosePopup();
            _login = new Login();
            _loadFollower = new LoadFollowers();
            _loadFollowee = new LoadFollowing();
            _loadLogin = new LoadLogin();
            _saveMedia = new SaveImage();
            _loadDetails = new LoadProfile();

            const string stateFile = "state.bin";
            if (File.Exists(stateFile))
            {
                try
                {
                    _currentUser = new User();
                    if (!_currentUser.login) return;
                    userLogin = (int)UserLogin.LoggedIn;
                    popupShow = "Hidden";
                    userName = _currentUser.currentUser.Value.UserName;
                    profilePicUrl = _currentUser.currentUser.Value.ProfilePicUrl;
                    //loadFeed().GetAwaiter().GetResult();
                    var act = new Action(async () => await loadFeed());
                    act.Invoke();
                }
                catch (System.NullReferenceException e)
                {
                    userLogin = (int)UserLogin.ConnectionError;
                }
            } 
        }


        public async void Login(string username, string password)
        {
            const string stateFile = "state.bin";

            if (!File.Exists(stateFile)) return;
            try
            {
                _currentUser = new User();
                if (!_currentUser.login) return;
                userLogin = (int)UserLogin.LoggedIn;
                popupShow = "Hidden";
                userName = _currentUser.currentUser.Value.UserName;
                profilePicUrl = _currentUser.currentUser.Value.ProfilePicUrl;
                await loadFeed();
            }
            catch (System.NullReferenceException e)
            {
                //System.Windows.MessageBox.Show("Error in Sending Request: Check your connection and try again");
                userLogin = (int)UserLogin.ConnectionError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"> Username for current user trying to login</param>
        public async void Login(string username)
        {

            _currentUser = new User(username, SecurePassword);

            if (_currentUser.login)
            {
                userLogin = (int)UserLogin.LoggedIn;
                popupShow = "Hidden";
                userName = _currentUser.currentUser.Value.UserName;
                profilePicUrl = _currentUser.currentUser.Value.ProfilePicUrl;
                await loadFeed();
            }
            else
            {
                errorMessage = _currentUser.loginResult;
            }

        }



        public async void loadViews()
        {
            switch (_selectedView)
            {
                case 1:
                    await loadFeed();
                    break;
                case 2:
                    await loadUserDetails();
                    break;
                case 3:
                    await loadFollowActivity();
                    break;
                case 4:
                    loadExplore();
                    break;
                case 5:
                    loadMessages();
                    break;
            }
        }

        private async Task loadFeed()
        {


            await Task.Run(PopulateFeed);
        }

        private void loadMessages()
        {
            throw new NotImplementedException();
        }

        private void loadExplore()
        {
            throw new NotImplementedException();
            //populateExploreFeed();

        }

        private void loadProfile()
        {
            throw new NotImplementedException();
        }


        public async Task loadPostComments(string mediaID)
        {
            await Task.Run(() => PopulateComments(mediaID) );
        }

        public async Task loadFollowActivity()
        {
            await Task.Run(PopulateFollowActivity);
        }

        public async Task loadPostLikers(string mediaID)
        {
            await Task.Run(() => PopulateLikers(mediaID));
        }

        private async Task loadUserDetails()
        {
            await Task.Run(() => populateUserDetails("trevortaks"));
        }

        public async Task loadUserDetails(string uname)
        {
            await Task.Run(() => populateUserDetails(uname));
        }

        public async Task loadUserFollowers(string uname)
        {
            await Task.Run( () => PopulateUserFollowers(uname));
        }

        public async Task loadUserFollowing(string uname)
        {
            await Task.Run(() => PopulateUserFollowing(uname));
        }

        internal void populateUserDetails(string username) 
        {
            var user =  _currentUser.getUserDetails(username);
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

        public async Task PopulateFeed()
        {
            
                if (SynchronizationContext.Current == _synchronizationContext)
                {
                    // Execute the CollectionChanged event on the current thread
                    //UpdateSections(sections);
                    foreach (var post in await _currentUser.getFeed())
                        Posts.Add(post);
                }
                else
                {
                    // Post the CollectionChanged event on the creator thread
                    foreach (var post in await _currentUser.getFeed())
                        _synchronizationContext.Post((e) => {
                            var item = (Post)e;
                            Posts.Add(item);
                        }, post);
                }
        }

        /// <summary>
        /// An async function that populates the collection comments <see cref=""/>
        /// </summary>
        /// <param name="mediaID"> Unique identifier for a post</param>
        /// <returns></returns>
        public async Task PopulateComments(string mediaID)
        {
            Comments.Clear();
            Comments.Concat(await _currentUser.getComments(mediaID));

        }

        public async Task PopulateLikers(string mediaID)
        {
            Likers.Clear();
            Likers.Concat(await _currentUser.getLikers(mediaID));  
        }

        public async Task PopulateFollowActivity(){

            var activities = await _currentUser.getUserFollowingActivity();

            foreach (var activity in activities.Value.Items) {
                LikeActivity.Add(activity.Text);
            }
        }

        

        public async Task PopulateUserFollowers(string username)
        {
            var followers = await _currentUser.GetFollowers();

            Likers.Clear();
            foreach (var follower in followers.Value)
            {
                Likers.Add(follower.UserName);
            }
        }

        public async Task PopulateUserFollowing(string username)
        {
            var following = await _currentUser.GetFollowers();
            Likers.Clear();
            foreach (var followed in following.Value)
            {
                Likers.Add(followed.UserName);
            }
        }

        public async Task PopulateExploreFeed() 
        {
           FeedPosts.Concat( await _currentUser.getExploreFeed());
           
        }

        public async Task LikeMedia(string mediaID)
        {
            var result = await Task.Run(() => _currentUser.likePost(mediaID));
        }

        /// <summary>
        /// Initiates and completes the download of an image.
        /// </summary>
        /// <param name="url"> Location of desiredd image </param>
        /// <returns></returns>
        public async Task GetImage(string url)
        {
            Image<Rgba32> image = await DownloadMedia(url);

            string rootpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string filename = RandomString();

            string filepath = System.IO.Path.Combine(rootpath + @"\Saved Pictures\" + filename);

            if (image != null)
            {
                try
                {
                    image.Save(filepath);
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {

                }
                catch (ArgumentNullException)
                {

                }
            }
        }

        
        /// <summary>
        /// Streams the image into a bitmap variable
        /// </summary>
        /// <param name="url">Location of media in post</param>
        /// <returns>Bitmap data for the downloaded image</returns>
        private Task<Image<Rgba32>> DownloadMedia(string url) 
        {
            Image<Rgba32> image;

            try
            {
                WebClient client = new WebClient();
                Stream str = client.OpenRead(url);

                image = (Image<Rgba32>)Image.Load(str);

            }
            catch (Exception) 
            {
                return null;
            }

            return Task.Run(() => { return image; });
        }

        /// <summary>
        /// Generates a random string to be used as name for downloaded media
        /// Function uses a stringbuilder
        /// A number is generated and and converted to equivalent unicode character
        /// Character is appended to stringbuilder
        /// </summary>
        /// <returns> String of random characters</returns>
        public static string RandomString()
        {
            var rand = new Random();
            StringBuilder sb = new StringBuilder();

            char ch;

            for (int i = 0; i < 11; i++)
            {
                //Generate a number and convert it to equivalent unicode chararacter
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 65)));
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 65)));
                sb.Append(ch);
            }

            sb.Append(".jpg");
            return sb.ToString().ToLower();

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
}
