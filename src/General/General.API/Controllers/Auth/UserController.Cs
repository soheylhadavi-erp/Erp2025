using MediatR;
using Microsoft.AspNetCore.Mvc;
using General.Application.Features.Auth.Commands.RegisterUser;
using General.Application.Features.Auth.Commands.LoginUser;
using Microsoft.AspNetCore.Authorization;
//using General.Application.Features.Auth.Queries.GetUserProfile;

namespace General.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// ثبت نام کاربر جدید
        /// </summary>
        [HttpPost("register")]
        //[Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        /// <summary>
        /// ورود کاربر به سیستم
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : Unauthorized(result);
        }

        // GET: api/User/profile/{id}
        // [HttpGet("profile/{id}")]
        // public async Task<IActionResult> GetProfile([FromRoute] Guid id)
        // {
        //     var query = new GetUserProfileQuery { UserId = id };
        //     var result = await _mediator.Send(query);
        //     return result != null ? Ok(result) : NotFound();
        // }
    }
}
