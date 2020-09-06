using SocialNetwork.Application.Interfaces;
using SocialNetwork.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Dtos
{
    public class UserDto
    {
        public UserDto(AppUser user, IJwtGenerator jwtGenerator)
        {
            DisplayName = user.DisplayName;
            Token = jwtGenerator.CreateToken(user);
            Username = user.UserName;
        }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
