using Common.Contract.Responses;
using General.Application.Security.Roles;
using General.Application.Security.Roles.Commands.CreateRole;
using General.Application.Security.Roles.Commands.DeleteRole;
using General.Contract.Roles;
using General.Contract.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// دریافت لیست صفحه‌بندی شده نقش‌ها
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<RoleResponse>>>> GetRoles(
            [FromQuery] GetPaginatedRolesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// دریافت نقش بر اساس شناسه
        /// </summary>
        [HttpGet("get-role-by-id")]
        public async Task<ActionResult<ApiResponse<RoleResponse>>> GetRoleById([FromQuery]GetRoleByIdQuey query)
        {
            var result = await _mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// ایجاد نقش جدید
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleResponse>>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Succeeded && result.Data != null)
                return CreatedAtAction(nameof(GetRoleById), new { id = result.Data.Id }, result);

            return BadRequest(result);
        }

        /// <summary>
        /// به‌روزرسانی نقش
        /// </summary>
        [HttpPost("update")]
        public async Task<ActionResult<ApiResponse>> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// حذف نقش
        /// </summary>
        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse>> DeleteRole([FromBody]DeleteRoleCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// افزودن کاربر به نقش
        /// </summary>
        [HttpPost("add-user-to-role")]
        public async Task<ActionResult<ApiResponse>> AddUserToRole(Guid roleId, Guid userId)
        {
            var command = new AddUserToRoleCommand { RoleId = roleId, UserId = userId };
            var result = await _mediator.Send(command);

            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// حذف کاربر از نقش
        /// </summary>
        [HttpPost("remove-user-from-role")]
        public async Task<ActionResult<ApiResponse>> RemoveUserFromRole([FromBody]RemoveUserFromRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// دریافت کاربران یک نقش
        /// </summary>
        [HttpGet("get-users-in-role")]
        public async Task<ActionResult<ApiResponse<List<UserResponse>>>> GetUsersInRole([FromQuery]GetUsersInRoleQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}