using AutoMapper;
using MediatR;
using SocialNetwork.Application.Errors;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Followers
{
    public class DeleteFollow
    {
        public class Command : IRequest
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository<AppUser> _userRepo;
            private readonly IRepository<UserFollowing> _followRepo;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(IRepository<AppUser> userRepo, IRepository<UserFollowing> followRepo, IUserAccessor userAccessor, IMapper mapper)
            {
                _userRepo = userRepo;
                _followRepo = followRepo;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var observer = await _userRepo.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());
                var target = await _userRepo.GetSingleOrDefaultAsync(x => x.UserName == request.Username);

                if(target == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                var following = await _followRepo.GetSingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);
                
                if(following == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { User = "You are not following this user" });

                _followRepo.Delete(following);
                var success = await _followRepo.SaveAsync();

                if (!success)
                    throw new Exception("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
