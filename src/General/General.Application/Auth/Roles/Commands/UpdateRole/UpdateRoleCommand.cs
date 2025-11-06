using Common.Contract;
using MediatR;

namespace General.Application.Auth.Roles
{
    public class UpdateRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public string Description { get; set; }
    }
}
