using Common.Contract;
using General.Contract.Auth.Users;
using MediatR;

namespace General.Application.Security.Roles
{
    public class GetUsersInRoleQuery : IRequest<ApiResponse<List<UserResponse>>>
    {
        public Guid RoleId { get; set; }
    }

}
