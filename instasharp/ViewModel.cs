using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instasharp
{
    public class ViewModel
    {
        User _currentUser;
        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> post
        {
           get {
                return _posts;
            }
        }

        public ViewModel() {
            //_currentUser = new User("trevortaks", "sti");
            //var feed = Task.Run(()=>populateFeed()).GetAwaiter().GetResult();
            //if(!feed) loadList();
            loadList();
        }

        

        public void loadList(){
         
            _posts.Add(new Post()
            {
                likesCount =  300,
                caption = "This is a caption",
                commentsCount = "200",
                userName = "trevortals",
                url = "/assets/image.jpg",
                isImage = true
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image2.jpg",
                //url = "/assets/image2.jpg",
                isImage = true
            });

            _posts.Add(new Post()
            {
                likesCount = 60,
                caption = "This is a caption 3",
                commentsCount = "20",
                userName = "trs",
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\video2.mp4",
                isImage = false
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                url = "/assets/video_image.jpg",
                //url = "/assets/image3.jpg",
                isImage = true
            });

            _posts.Add(new Post()
            {
                likesCount = 30,
                caption = "This is a caption 2",
                commentsCount = "100",
                userName = "trevorts",
                //url = "/assets/video_image.jpg"
                url = @"C:\Users\Trevor Takawira\documents\visual studio 2012\Projects\instasharp\instasharp\assets\image4.jpg",
                isImage = true
            });

          // MainWindow.icPost.ItemsSource = posts;
        }

        public async Task<bool> populateFeed()
        {
            var feed = await _currentUser.getFeed();
            //List<Post> posts = new List<Post>();
            if (feed.Succeeded)
            {

                foreach (var media in feed.Value.Medias)
                {
                    int likes = media.LikesCount;
                    string captionT = media.Caption.Text;
                    string comments = media.CommentsCount;
                    string name = media.User.UserName;
                    List<string> imgURLs = new List<string>();
                    foreach (var item in media.Images)
                        imgURLs.Add(item.URI);
                    _posts.Add(new Post()
                    {
                        likesCount = likes,
                        caption = captionT,
                        commentsCount = comments,
                        userName = name
                    });
                }
                return true;
            }
            return false;
        }

    }

    public class Post
    {
        public int likesCount { get; set; }
        public string caption { get; set; }
        public string commentsCount { get; set; }
        public string userName { get; set; }
        public string url { get; set; }
        public bool isImage { get; set; }
    }
}
