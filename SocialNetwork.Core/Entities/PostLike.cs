using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Core.Entities
{
    public class PostLike
    {
        public int LikerId { get; set; }
        public int PostId { get; set; }
        public AppUser Liker { get; set; }
        public Post Post { get; set; }
    }
}
