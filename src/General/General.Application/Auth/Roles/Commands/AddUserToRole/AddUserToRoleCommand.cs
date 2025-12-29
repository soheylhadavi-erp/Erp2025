using Common.Contract;
using MediatR;

namespace General.Application.Auth.Roles
{
    public class AddUserToRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
