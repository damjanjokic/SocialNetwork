using AutoMapper;
using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Posts
{
    public class CreatePost
    {
        public class Command : IRequest<PostDto>
        {
            public string Text { get; set; }
        }

        public class Handler : IRequestHandler<Command, PostDto>
        {
            private readonly IRepository<Post> _postRepository;
            private readonly IRepository<AppUser> _userRepository;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(IRepository<Post> postRepository, IRepository<AppUser> userRepository, IUserAccessor userAccessor, IMapper mapper)
            {
                _postRepository = postRepository;
                _userRepository = userRepository;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<PostDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = _mapper.Map<Post>(request);
                post.Created = DateTime.Now;

                post.Author = await _userRepository.GetSingleOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

                _postRepository.Add(post);

                var success = await _postRepository.SaveAsync();

                if(success)
                    return _mapper.Map<PostDto>(post);

                throw new Exception("Problem Saving Changes");
            }
        }
    }
}
