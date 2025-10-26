using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : UpdateRoleRequest,IRequest<RoleOperationResult>
    {
    }
}
