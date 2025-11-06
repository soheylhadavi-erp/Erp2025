using Common.Contract;
using MediatR;

namespace General.Application.Auth.Roles
{
    public class DeleteRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
    }

}
