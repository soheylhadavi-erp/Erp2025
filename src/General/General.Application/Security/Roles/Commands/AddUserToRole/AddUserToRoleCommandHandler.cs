using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using MediatR;
namespace General.Application.Security.Roles.Commands.DeleteRole
{
    public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, ApiResponse>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AddUserToRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.AddUserToRoleAsync(request.RoleId,request.UserId);
            var response = _mapper.Map<OperationResultDto, ApiResponse>(result);
            return response;
        }
    }
}