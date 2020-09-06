using AutoMapper;
using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Followers
{
    public class GetFollows
    {
        public class Query : IRequest<List<string>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<string>>
        {
            private readonly IRepository<UserFollowing> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepository<UserFollowing> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userFollowing = new List<UserFollowing>();
                var profiles = new List<string>();

                switch (request.Predicate)
                {
                    case "followers":
                        {
                            userFollowing = (List<UserFollowing>) await _repository.QueryAsync(x => x.Target.UserName == request.Username, includes: (y => y.Observer));
                            foreach(var follower in userFollowing)
                            {
                                profiles.Add(follower.Observer.UserName);
                            }
                            break;
                        }
                    case "following":
                        {
                            userFollowing = (List<UserFollowing>)await _repository.QueryAsync(x => x.Observer.UserName == request.Username, includes: (y => y.Target));
                            foreach (var follower in userFollowing)
                            {
                                profiles.Add(follower.Target.UserName);
                            }
                            break;
                        }
                }
                return profiles;
            }

           
        }

    }
}
