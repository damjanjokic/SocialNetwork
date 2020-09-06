using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialNetwork.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
