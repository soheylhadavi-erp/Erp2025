using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using MediatR;
namespace General.Application.Security.Roles.Commands.DeleteRole
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