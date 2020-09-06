using MediatR;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Errors;
using System.Net;
using SocialNetwork.Core.IRepositories;

namespace SocialNetwork.Application.User
{
    public class Login
    {
        public class Query : IRequest<UserDto>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly IAuthRepository _authRepository;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(IAuthRepository authRepository, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _authRepository = authRepository;
                
            }


            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _authRepository.FindByUsername(request.Username);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized);

                var result = await _authRepository
                    .CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return new UserDto(user, _jwtGenerator);
                }

                throw new RestException(HttpStatusCode.Unauthorized);


            }
        }
    }
}
