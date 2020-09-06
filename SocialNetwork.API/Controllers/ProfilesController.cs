using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Profiles;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            return Ok(await _mediator.Send(new GetProfile.Query { Username = username }));
        }

    }
}
