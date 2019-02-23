using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharper;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.API.Builder;
using InstaSharper.Logger;
using InstaSharper.API;
using System.IO;

namespace instasharp
{
    public class User
    {
        private static IInstaApi _instaApi;
        IResult<InstaCurrentUser> currentUser = null;
        public string loginResult { get; set; }

        public User(string uname, string pwd) {

            if (uname == "" || pwd == "")
            {
                loginResult = "Enter All Fields";
            }
            else
            {
                var result = Task.Run(() => Login(uname, pwd)).GetAwaiter().GetResult();

                if (result)
                {
                    loginResult = "Success";
                }
               /* else
                {
                    loginResult = "Wrong Password or Email";
                }*/
            }
        }

        private async Task<bool>  Login(string username, string password)
        {
            // create user session data and provide login details
            var userSession = new UserSessionData
            {
                UserName = username,
                Password = password
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
                    Console.WriteLine("Loading state from file");
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
                    // login
                    Console.WriteLine("Logging in as + {userSession.UserName}");
                    delay.Disable();
                    var logInResult = await _instaApi.LoginAsync();
                    delay.Enable();
                    if (!logInResult.Succeeded)
                    {
                        loginResult = "Unable to login:" + logInResult.Info.Message;
                       return false;
                    }
                }
            return true;
        }

        public async Task<IResult<InstaUserShortList>> getFollowers()
        {
            currentUser = await _instaApi.GetCurrentUserAsync();
            var followers = await _instaApi.GetUserFollowersAsync(
                currentUser.Value.UserName, 
                PaginationParameters.MaxPagesToLoad(5)
                );

            return followers;
        }

        public async Task<IResult<InstaUserShortList>> getFollowing() {
            currentUser = await _instaApi.GetCurrentUserAsync();
            var following = await _instaApi.GetUserFollowingAsync(
                currentUser.Value.UserName,
                PaginationParameters.MaxPagesToLoad(5)
                );
            return following;
        }

        public async Task<IResult<InstaFeed>> getFeed() {
            var userFeed = await _instaApi.GetUserTimelineFeedAsync(PaginationParameters.MaxPagesToLoad(5));
            return userFeed;
        }

        public async Task<IResult<InstaMediaList>> getPosts()
        {
            var userPosts = await _instaApi.GetUserMediaAsync(
                currentUser.Value.UserName, 
                PaginationParameters.MaxPagesToLoad(5)
                );
            return userPosts;
        }

        public async Task likePost(InstaMedia media) {
            var likeResult = await _instaApi.LikeMediaAsync(media.InstaIdentifier);
        }

        public async Task unlikePost(InstaMedia media) {
            var unlikeResult = await _instaApi.UnLikeMediaAsync(media.InstaIdentifier);
        }
    }

}
