using AutoMapper;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.Followers;
using SocialNetwork.Application.Posts;
using SocialNetwork.Application.User;
using SocialNetwork.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<CreatePost.Command, Post>();
            CreateMap<EditPost.Command, Post>();

            CreateMap<Register.Command, AppUser>();

            CreateMap<AddFollow.Command, UserFollowing>();

            CreateMap<AppUser, AppUserDto>();

            CreateMap<AppUser, ProfileDto>();
        }
    }
}
