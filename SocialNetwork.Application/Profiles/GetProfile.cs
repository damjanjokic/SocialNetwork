using AutoMapper;
using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.Errors;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Profiles
{
    public class GetProfile
    {
        public class Query : IRequest<ProfileDto>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProfileDto>
        {
            private readonly IRepository<AppUser> _userRepository;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(IRepository<AppUser> userRepository, IUserAccessor userAccessor, IMapper mapper)
            {
                _userRepository = userRepository;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<ProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetSingleOrDefaultAsync(x => x.UserName == request.Username,  (y => y.Followers), (y => y.Followings));

                if(user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found"});

                var currentUser = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());


                var profile = _mapper.Map<ProfileDto>(user);
                profile.FollowersCount = user.Followers.Count();
                profile.FollowingCount = user.Followings.Count();

                if (currentUser.Followings.Any(x => x.TargetId == user.Id))
                    profile.IsFollowed = true;

                return profile;
            }
        }
    }
}
