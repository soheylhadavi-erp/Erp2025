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
            return await _roleService.CreateRoleAsync(new CreateRoleRequestDto
            {
                Name= request.Name,
                Description= request.Description,
            });
        }
    }
}
