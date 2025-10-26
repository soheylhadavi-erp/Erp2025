using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>> { }
}
