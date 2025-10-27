using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<RoleOperationResult>
    {
        public Guid RoleId { get; set; }
        public string Description { get; set; }
    }
}
