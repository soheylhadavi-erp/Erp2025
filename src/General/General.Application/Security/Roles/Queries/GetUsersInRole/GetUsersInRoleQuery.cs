using Common.Contract.Responses;
using General.Contract.Roles;
using General.Contract.Users;
using MediatR;

namespace General.Application.Security.Roles
{
    public class GetUsersInRoleQuery : IRequest<ApiResponse<List<UserResponse>>>
    {
        public Guid RoleId { get; set; }
    }

}
