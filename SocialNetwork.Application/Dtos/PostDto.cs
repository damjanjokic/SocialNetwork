using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public AppUserDto Author { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
