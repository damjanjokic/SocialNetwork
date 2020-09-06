using AutoMapper;
using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.Errors;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Posts
{
    public class GetPostById
    {
        public class Query : IRequest<PostDto>
        {
            public int Id { get; set; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, PostDto>
        {
            private readonly IRepository<Post> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepository<Post> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PostDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = await _repository.GetSingleOrDefaultAsync(x => x.Id == request.Id, y => y.Author, a => a.Comments);

                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Post = "Not found" });

                return _mapper.Map<PostDto>(post);
            }
        }
    }
}
