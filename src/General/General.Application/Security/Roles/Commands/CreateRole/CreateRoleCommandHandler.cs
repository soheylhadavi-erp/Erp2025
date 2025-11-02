using AutoMapper;
using Common.Application.Models;
using Common.Application.Utilities;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using General.Application.Security.Roles.Models;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Security.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<RoleResponse>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.CreateRoleAsync(new CreateRoleRequestDto
            {
                Name = request.Name,
                Description = request.Description,
            });
            var response = _mapper.Map<OperationResultDto<RoleDto>, ApiResponse<RoleResponse>>(result);
            return response;
        }
    }
}
