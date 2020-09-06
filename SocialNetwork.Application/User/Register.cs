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

namespace SocialNetwork.Application.User
{
    public class Register
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAuthRepository _authRepository;
            private readonly IMapper _mapper;

            public Handler(IAuthRepository authRepository, IMapper mapper)
            {
                _authRepository = authRepository;
                _mapper = mapper;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                if (await _authRepository.UserExistsQuery(x => x.NormalizedEmail == request.Email.ToUpper()))
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

                if (await _authRepository.UserExistsQuery(x => x.NormalizedUserName == request.Username.ToUpper()))
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

                var user = _mapper.Map<AppUser>(request);
                var result = await _authRepository.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    throw new Exception("Problem creating user");

                return Unit.Value;
            }
        }
    }
}
