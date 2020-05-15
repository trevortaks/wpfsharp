using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        public string mediaID { get; set; }
        public int likesCount { get; set; }
        public string caption { get; set; }
        public string commentsCount { get; set; }
        public string userName { get; set; }
        public string userPic { get; set; }
        public string url { get; set; }
        public bool isImage { get; set; }
        public bool isLiked { get; set; }
        public DateTime date { get; set; }
    }
}
