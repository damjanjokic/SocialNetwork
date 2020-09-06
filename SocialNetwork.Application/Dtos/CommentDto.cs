using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AppUserDto Author { get; set; }
        public PostDto Post { get; set; }
    }
}
