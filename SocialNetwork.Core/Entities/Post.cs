using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(225)]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public AppUser Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
    }
}
