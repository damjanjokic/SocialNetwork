using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Followers;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{username}/follow")]
        public async Task<ActionResult<Unit>> Follow(string username)
        {
            return await _mediator.Send(new AddFollow.Command { Username = username });
        }

        [HttpDelete("{username}/follow")]
        public async Task<ActionResult<Unit>> Unfollow(string username)
        {
            return await _mediator.Send(new DeleteFollow.Command { Username = username });
        }

        [HttpGet("{username}/{predicate}")]
        public async Task<IActionResult> GetFollowings(string predicate, string username)
        {
            return Ok(await _mediator.Send(new GetFollows.Query { Username = username, Predicate = predicate }));
        }

    }
}
