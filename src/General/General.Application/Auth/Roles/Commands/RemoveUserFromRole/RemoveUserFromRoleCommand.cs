using Common.Contract;
using MediatR;

namespace General.Application.Security.Roles
{
    public class RemoveUserFromRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }

}
