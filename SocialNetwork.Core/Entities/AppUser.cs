using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        [StringLength(20)]
        public string DisplayName { get; set; }
        [StringLength(100)]
        public string Bio { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public  ICollection<UserFollowing> Followings { get; set; }
        public  ICollection<UserFollowing> Followers { get; set; }

       /* public AppUser()
        {
            Followings = new Collection<UserFollowing>();
            Followers = new Collection<UserFollowing>();
        }*/

    }
}
