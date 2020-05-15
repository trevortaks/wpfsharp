using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    public class CommentsVM : ViewModel
    {
        string mediaID;

        public ObservableCollection<Comment> comments { get; } = new ObservableCollection<Comment>();



        public CommentsVM() {
            //this.mediaID = mediaID;

        }

        private async Task loadComments() {
            this.comments.Clear();
            comments.Concat(await _currentUser.getComments(mediaID));
  
        }

        public void ppComments()
        {
            comments.Clear();
            for (int i = 0; i < 20; i++)
            {
                comments.Add(new Comment
                {
                    userName = "user" + i,
                    comment = "This is a comment"
                });

            }
        }


    }
}
