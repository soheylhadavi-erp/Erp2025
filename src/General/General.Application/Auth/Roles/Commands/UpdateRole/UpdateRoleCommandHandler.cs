using AutoMapper;
using Common.Application;
using Common.Contract;
using General.Application.Auth.Roles;
using MediatR;
namespace General.Application.Security.Roles
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateRoleAsync(request.RoleId, request.Description);
            var response = _mapper.Map<OperationResultDto, ApiResponse>(result);
            return response;
        }
    }
}