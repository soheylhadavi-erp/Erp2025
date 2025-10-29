using Common.Contract.Responses;
using MediatR;

namespace General.Application.Security.Roles
{
    public class AddUserToRoleCommand : IRequest<ApiResponse>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
