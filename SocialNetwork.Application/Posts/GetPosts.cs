using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Posts
{
    public class GetPosts
    {
        public class Query : IRequest<IEnumerable<PostDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<PostDto>>
        {
            private readonly IRepository<Post> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepository<Post> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PostDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _repository.GetAllAsync();

                return _mapper.Map<IEnumerable<PostDto>>(posts);
            }
        }
    }
}
