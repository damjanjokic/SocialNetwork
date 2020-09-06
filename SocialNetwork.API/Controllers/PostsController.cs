using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Posts;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _mediator.Send(new GetPosts.Query());

            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePost.Command command)
        {
            var post = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPost(int id)
        {

            return Ok(await _mediator.Send(new GetPostById.Query(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost([FromBody] EditPost.Command command, int id)
        {
            command.Id = id;

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            return Ok(await _mediator.Send(new DeletePost.Command(id)));
        }

    }
}
