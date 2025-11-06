using Common.Contract;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Auth.Roles
{
    public class CreateRoleCommand : ICreateRoleInput, IRequest<ApiResponse<RoleResponse>>
    {
    }
}
