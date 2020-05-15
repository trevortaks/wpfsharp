using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Logger;
using System.IO;
using System.Security;
using Models;

namespace Logic
{
    public class User
    {
        private static IInstaApi _instaApi;
        public IResult<InstaCurrentUser> currentUser { get; private set; } = null;

        public string loginResult { get; private set; }
        public bool login { get; private set; }

        public User(string uname, SecureString pwd) {

            if (uname == "" || pwd.Length == 0)
            {
                loginResult = "Enter All Fields";
                return ;
            }
            else
            {
                login = Task.Run(() => Login(uname, pwd)).GetAwaiter().GetResult();
            }
        }

        public User() {
            login = Task.Run(Login).GetAwaiter().GetResult();
        }

        private async Task<bool> Login() 
        {
            var delay = RequestDelay.FromSeconds(2, 2);
            // create new InstaApi instance using Builder
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(UserSessionData.Empty)
                .UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
                .SetRequestDelay(delay)
                .Build();

            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    Console.WriteLine(@"Loading state from file");
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                    currentUser = await _instaApi.GetCurrentUserAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false; 
        }

        private async Task<bool>  Login(string username, SecureString password)
        {
            // create user session data and provide login details
            var userSession = new UserSessionData
            {
                UserName = username,
                Password = new System.Net.NetworkCredential(string.Empty, password).Password
            };

            var delay = RequestDelay.FromSeconds(2, 2);
            // create new InstaApi instance using Builder
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
                .SetRequestDelay(delay)
                .Build();

            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    Console.WriteLine(@"Loading state from file");
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (!_instaApi.IsUserAuthenticated)
                {
                    delay.Disable();
                    var logInResult = await _instaApi.LoginAsync();
                    delay.Enable();
                    if (!logInResult.Succeeded)
                    {
                        loginResult = logInResult.Info.Message;
                       return false;
                    }
                }
            var state = _instaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }
            currentUser = await _instaApi.GetCurrentUserAsync();
            return true;
        }

        public async Task<IResult<InstaUserShortList>> GetFollowers()
        {
            currentUser = await _instaApi.GetCurrentUserAsync();
            var followers = await _instaApi.UserProcessor.GetCurrentUserFollowersAsync(
                PaginationParameters.MaxPagesToLoad(5)
                );

            return followers;
        }

        public async Task<IResult<InstaUserShortList>> GetUserFollowers(string username) 
        {
            var followers = await _instaApi.UserProcessor.GetUserFollowersAsync(
                username,
                PaginationParameters.MaxPagesToLoad(5)
                );

            return followers;
        }

        public async Task<IResult<InstaUserInfo>> getUserDetails(string username) 
        {
            //var details = await _instaApi.GetUserAsync(username);
            var details = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync(username);
            return details;
        }

        public async Task<IResult<InstaMediaList>> getUserPosts(string username) 
        {
            var posts = await _instaApi.UserProcessor.GetUserMediaAsync(
                username,
                PaginationParameters.MaxPagesToLoad(5)
                );
            return posts;
        }

        public async Task getChallenge() 
        {
            var challenge = await _instaApi.GetChallengeRequireVerifyMethodAsync();
            if (challenge.Succeeded) 
            {
                
            }
        }

        public async Task<IResult<InstaUserShortList>> GetUserFollowing(string username) {
            //_currentUser = await _instaApi.GetCurrentUserAsync();
            var following = await _instaApi.UserProcessor.GetUserFollowersAsync(
                username,
                PaginationParameters.MaxPagesToLoad(5)
                );
            return following;
        }

        public async Task<IResult<InstaActivityFeed>> getUserFollowingActivity()
        {
            var activity = await _instaApi.UserProcessor.GetFollowingRecentActivityFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            return activity;
        }

        public async Task<List<string>> getUserCurrentActivity() 
        {
            var activity = await _instaApi.UserProcessor.GetRecentActivityFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            List<string> activities = new List<string>();

            foreach (var act in activity.Value.Items)
            {
                activities.Add(act.Text);
            }

            return activities;
        }


        public async Task<List<Post>> getFeed() {
            var userFeed = await _instaApi.FeedProcessor.GetUserTimelineFeedAsync(PaginationParameters.MaxPagesToLoad(1));

            List<Post> feedPosts = new List<Post>();

            if (userFeed.Succeeded)
            {

                foreach (var media in userFeed.Value.Medias)
                {
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
                        feedPosts.Add(new Post()
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
                var error = userFeed.Info.Message;
            }
            return feedPosts;
        }

        public async Task<IResult<InstaMediaList>> getPosts()
        {
            var userPosts = await _instaApi.UserProcessor.GetUserMediaAsync(
                currentUser.Value.UserName, 
                PaginationParameters.MaxPagesToLoad(2)
                );
            return userPosts;
        }

        public async Task<List<Post>> getExploreFeed() {

            var feed = await _instaApi.FeedProcessor.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(5));

            List<Post> feedPosts = new List<Post>();

            foreach (var post in feed.Value.Medias)
            {
                feedPosts.Add(
                    new Post
                    {
                        caption = post.Caption.Text,
                        url = post.Images[0].Uri,
                        likesCount = post.LikesCount,
                        mediaID = post.InstaIdentifier
                    }
                    );
            }
            return feedPosts;
        }

        public async Task<IResult<InstaDirectInboxContainer>> getMessageFeed() {
            var messageList = await _instaApi.MessagingProcessor.GetDirectInboxAsync(PaginationParameters.MaxPagesToLoad(1));
            return messageList;
        }

        public async Task<string> likePost(string mediaID) {
            var likeResult = await _instaApi.MediaProcessor.LikeMediaAsync(mediaID);
            var result = likeResult.Value ? "Liked" : "Not Liked";
            return result;
        }

        public async Task unlikePost(InstaMedia media) {
            var unlikeResult = await _instaApi.MediaProcessor.UnLikeMediaAsync(media.InstaIdentifier);
        }

        public async Task<List<string>> getLikers(string mediaID){
    
            var result = await _instaApi.MediaProcessor.GetMediaLikersAsync(mediaID);

            List<string> likers = new List<string>();
            foreach (var liker in result.Value)
            {
                likers.Add(liker.UserName);
            }
            return likers;
        }

        public async Task<List<Comment>> getComments(string mediaID) 
        {
            var result = await _instaApi.CommentProcessor.GetMediaCommentsAsync(
                mediaID, 
                PaginationParameters.MaxPagesToLoad(2)
                );

            List<Comment> comments = new List<Comment>();
            foreach (var comment in result.Value.Comments)
            {
                comments.Add(
                        new Comment
                        {
                            userName = comment.User.UserName,
                            comment = comment.Text
                        }
                    );
            }

            return comments;
        }
        
        public async Task<string> uploadPic()
        {
            var mediaImage = new InstaImageUpload
            {
                Height = 1080,
                Width = 1080,
                Uri = new Uri(Path.GetFullPath(@"c:\someawesomepicture.jpg"), UriKind.Absolute).LocalPath
            };
            var result = await _instaApi.MediaProcessor.UploadPhotoAsync(mediaImage, "someawesomepicture");
            var str = result.Succeeded ? result.Value.Pk : result.Info.Message;

            return str;
        }

        
    }

}
