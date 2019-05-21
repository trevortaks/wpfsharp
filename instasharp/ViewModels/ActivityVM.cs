using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instasharp.ViewModels
{
    class ActivityVM : ViewModel
    {
        private ObservableCollection<string> _likeActivity = new ObservableCollection<string>();
        public ObservableCollection<string> likeActivity
        {
            get { return _likeActivity; }
        }


        private async Task populateFollowActivity()
        {

            var activities = await _currentUser.getUserFollowingActivity();

            foreach (var activity in activities.Value.Items)
            {
                _likeActivity.Add(activity.Text);
            }
        }
    }
}
