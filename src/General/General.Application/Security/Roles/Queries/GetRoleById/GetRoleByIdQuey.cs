using Common.Contract.Responses;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Security.Roles
{
    public class GetRoleByIdQuey : IRequest<ApiResponse<RoleResponse>>
    {
        public Guid RoleId { get; set; }
    }

}
