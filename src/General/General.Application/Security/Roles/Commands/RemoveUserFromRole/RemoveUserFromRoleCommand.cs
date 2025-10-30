using Common.Contract.Responses;
using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Commands.DeleteRole
{
    public class RemoveUserFromRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }

}
