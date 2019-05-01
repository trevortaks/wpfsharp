﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instasharp.Models
{
    public class UserDetails
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string profilePic { get; set; }
        public string followers { get; set; }
        public string following { get; set; }
        public string postsCount { get; set; }
        public bool isFollowing { get; set; }
        public string biography { get; set; }
        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> posts { get { return _posts; } }


    }
}
