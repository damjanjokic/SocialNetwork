using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Dtos;
using SocialNetwork.Application.User;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login.Query query)
        {
            var user = await _mediator.Send(query);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register.Command command)
        {
            await _mediator.Send(command);

            return Ok("Registration successful");
        }

    }
}
