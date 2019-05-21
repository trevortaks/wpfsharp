using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using instasharp.Models;

namespace instasharp.ViewModels
{
    class CommentsVM : ViewModel
    {
        string mediaID;

        private ObservableCollection<Comment> _comments = new ObservableCollection<Comment>();
        public ObservableCollection<Comment> comments
        {
            get { return _comments; }
        }



        public CommentsVM() {
            //this.mediaID = mediaID;

        }

        private async Task loadComments() {
            this.comments.Clear();
            var comments = await _currentUser.getComments(mediaID);

            if (comments.Succeeded) 
            {
                foreach(var comment in comments.Value.Comments){
                    this.comments.Add(
                            new Comment()
                            {
                                userName = comment.User.UserName,
                                comment = comment.Text
                            }
                        );
                }
            }
        }

        public void ppComments()
        {
            _comments.Clear();
            for (int i = 0; i < 20; i++)
            {
                _comments.Add(new Comment
                {
                    userName = "user" + i,
                    comment = "This is a comment"
                });

            }
        }


    }
}
