using AutoMapper;
using MediatR;
using SocialNetwork.Application.Errors;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Posts
{
    public class EditPost
    {
        public class Command : IRequest<bool>
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Post> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepository<Post> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _repository.GetByIdAsync(request.Id);

                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Post = "Not found" });

                _mapper.Map<Command, Post>(request, post);

                var success = await _repository.SaveAsync();

                if (!success)
                    throw new Exception("Problem saving changes");

                return success;
            }
        }
    }
}
