using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using InstaSharper;
//using InstaSharper.Classes;
//using InstaSharper.Classes.Models;
//using InstaSharper.API.Builder;
//using InstaSharper.Logger;
//using InstaSharper.API;
using InstagramApiSharp;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Logger;
using System.IO;
using System.Security;

namespace instasharp
{
    public class User
    {
        private static IInstaApi _instaApi;
        IResult<InstaCurrentUser> _currentUser = null;
        public string loginResult { get; private set; }
        public bool login { get; private set; }

        public User(string uname, SecureString pwd) {

            if (uname == "" || pwd == null)
            {
                loginResult = "Enter All Fields";
            }
            else
            {
                login = Task.Run(() => Login(uname, pwd)).GetAwaiter().GetResult();
            }
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
            return true;
        }

        public async Task<IResult<InstaUserShortList>> getFollowers()
        {
            _currentUser = await _instaApi.GetCurrentUserAsync();
            var followers = await _instaApi.UserProcessor.GetCurrentUserFollowersAsync(
                PaginationParameters.MaxPagesToLoad(5)
                );

            return followers;
        }

        public static async Task<IResult<InstaUserShortList>> getUserFollowers(string username) 
        {
            var followers = await _instaApi.UserProcessor.GetUserFollowersAsync(
                username,
                PaginationParameters.MaxPagesToLoad(5)
                );

            return followers;
        }

        public static async Task<IResult<InstaUserInfo>> getUserDetails(string username) 
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

        public static async Task<IResult<InstaUserShortList>> getUserFollowing(string username) {
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

        public async Task<IResult<InstaActivityFeed>> getUserCurrentActivity() 
        {
            var activity = await _instaApi.UserProcessor.GetRecentActivityFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            return activity;
        }


        public async Task<IResult<InstaFeed>> getFeed() {
            var userFeed = await _instaApi.FeedProcessor.GetUserTimelineFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            return userFeed;
        }

        public async Task<IResult<InstaMediaList>> getPosts()
        {
            var userPosts = await _instaApi.UserProcessor.GetUserMediaAsync(
                _currentUser.Value.UserName, 
                PaginationParameters.MaxPagesToLoad(2)
                );
            return userPosts;
        }

        public static async Task<IResult<InstaExploreFeed>> getExploreFeed() {

            var feed = await _instaApi.FeedProcessor.GetExploreFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            return feed;
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

        public async Task<IResult<InstaLikersList>> getLikers(string mediaID){
    
            var result = await _instaApi.MediaProcessor.GetMediaLikersAsync(mediaID);
            return result;
        }

        public async Task<IResult<InstaCommentList>> getComments(string mediaID) 
        {
            var result = await _instaApi.CommentProcessor.GetMediaCommentsAsync(
                mediaID, 
                PaginationParameters.MaxPagesToLoad(2)
                );
            return result;
        }
        
        public async Task uploadPic()
        {
            var mediaImage = new InstaImage
            {
                Height = 1080,
                Width = 1080,
                Uri = new Uri(Path.GetFullPath(@"c:\someawesomepicture.jpg"), UriKind.Absolute).LocalPath
            };
            //var result = await _instaApi.MediaProcessor.UploadPhotoAsync(mediaImage, "someawesomepicture");
            //var str = result.Succeeded ? result.Value.Pk : result.Info.Message;

        }

        
    }

}
