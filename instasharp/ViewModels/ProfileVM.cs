using instasharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instasharp.ViewModels
{
    class ProfileVM : ViewModel, INotifyPropertyChanged
    {
        string uname;
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

        ProfileVM(string uname) 
        {
            this.uname = uname;
        }

        public async Task populateUserDetails() 
        {
            var user = User.getUserDetails(username);
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

    }
}
