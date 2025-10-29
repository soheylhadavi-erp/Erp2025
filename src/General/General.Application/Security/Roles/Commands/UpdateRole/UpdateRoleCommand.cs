using Common.Contract.Responses;
using MediatR;

namespace General.Application.Security.Roles
{
    public class UpdateRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public string Description { get; set; }
    }
}
