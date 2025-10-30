using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using MediatR;
namespace General.Application.Security.Roles.Commands.DeleteRole
{
    public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommand, ApiResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RemoveUserFromRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(request.RoleId,request.UserId);
            var response = _mapper.Map<OperationResultDto, ApiResponse>(result);
            return response;
        }
    }
}