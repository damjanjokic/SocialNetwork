using AutoMapper;
using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.Errors;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Posts
{
    public class DeletePost
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public Command(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository<Post> _repository;
            private readonly IMapper _mapper;

            public Handler(IRepository<Post> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _repository.GetByIdAsync(request.Id);

                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Post = "Not found" });

                _repository.Delete(post);
                var success = await _repository.SaveAsync();

                if (!success)
                    throw new Exception("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
