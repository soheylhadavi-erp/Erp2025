using AutoMapper;
using Common.Application;
using Common.Contract;
using General.Application.Auth.Roles;
using MediatR;
namespace General.Application.Security.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ApiResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public DeleteRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(request.RoleId);
            var response = _mapper.Map<OperationResultDto, ApiResponse>(result);
            return response;
        }
    }
}