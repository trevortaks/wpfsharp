using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels 
{
    public class LikersVM : ViewModel
    {
        string mediaID;
        public ObservableCollection<string> likers { get; } = new ObservableCollection<string>();

        LikersVM(string mediaID)
        {
            this.mediaID = mediaID;
        }

        public async Task populateLikers() {
            likers.Clear();
            likers.Concat( await _currentUser.getLikers(mediaID));
        }

        public async Task populateFollowers(string uname) 
        {
            likers.Clear();
            
            var following = await _currentUser.GetUserFollowers(uname);

            foreach (var followed in following.Value)
            {
                likers.Add(followed.UserName);
            }
        }

        public async Task populateFollowinng(string uname) 
        {
            likers.Clear();

            var following = await _currentUser.GetUserFollowing(uname);
            
            foreach (var followed in following.Value)
            {
                likers.Add(followed.UserName);
            }
        }
    }

}
