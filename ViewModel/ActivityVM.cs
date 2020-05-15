using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ActivityVM : ViewModel
    {
        public ObservableCollection<string> likeActivity { get; } = new ObservableCollection<string>();


        private async Task PopulateFollowActivity()
        {

            var activities = await _currentUser.getUserFollowingActivity();

            foreach (var activity in activities.Value.Items)
            {
                likeActivity.Add(activity.Text);
            }
        }
    }
}
