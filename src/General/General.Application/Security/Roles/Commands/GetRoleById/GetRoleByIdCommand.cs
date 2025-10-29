using Common.Contract.Responses;
using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles
{
    public class GetRoleByIdCommand : IRequest<ApiResponse<RoleDto>>
    {
        public Guid RoleId { get; set; }
    }

}
