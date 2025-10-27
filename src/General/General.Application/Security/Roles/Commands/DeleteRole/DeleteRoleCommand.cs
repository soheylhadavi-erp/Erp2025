using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<RoleOperationResult>
    {
        public Guid RoleId { get; set; }
    }

}
