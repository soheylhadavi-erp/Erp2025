using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Security.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : ICreateRoleInput, IRequest<ApiResponse<RoleResponse>>
    {
    }
}
