using AutoMapper;
using Common.Application;
using Common.Contract;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Auth.Roles
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
