using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instasharp.ViewModels 
{
    class LikersVM : ViewModel
    {
        string mediaID;
        private ObservableCollection<string> _likers = new ObservableCollection<string>();
        public ObservableCollection<string> likers
        {
            get { return _likers; }
        }

        LikersVM(string mediaID)
        {
            this.mediaID = mediaID;
        }

        public async Task populateLikers() {
            _likers.Clear();
            var likersList = await User.getLikers(mediaID);

            foreach (var liker in likersList.Value)
            {
                _likers.Add(liker.UserName);
            }
        }

        public void populateFollowers(string uname) 
        {
            var followers = User.getUserFollowers(uname);

            likers.Clear();
            foreach (var follower in followers.Result.Value)
            {
                likers.Add(follower.UserName);
            }
        }

        public void populateFollowinng(string uname) 
        {
            var following = User.getUserFollowing(uname);
            likers.Clear();
            foreach (var followed in following.Result.Value)
            {
                likers.Add(followed.UserName);
            }
        }
    }

}
