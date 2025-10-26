using General.Application.Security.Roles.Interfaces;
using General.Application.Security.Roles.Models;
using MediatR;

namespace General.Application.Security.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleOperationResult>
    {
        private readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RoleOperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var createRequest = new CreateRoleRequest
            {
                Name = request.Name,
                Description = request.Description
            };

            return await _roleService.CreateRoleAsync(request);//ممکنه مپینگ بخواد
        }
    }
}
