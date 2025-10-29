using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using General.Application.Security.Roles.Models;
using General.Contract.Roles;
using MediatR;
namespace General.Application.Security.Roles
{
    public class GetRoleByIdCommandHandler : IRequestHandler<GetRoleByIdCommand, ApiResponse<RoleResponse>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByIdCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RoleResponse>> Handle(GetRoleByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRoleByIdAsync(request.RoleId);
            var role = _mapper.Map<RoleDto, RoleResponse>(result);
            var response= ApiResponse<RoleResponse>.Success(role,"");
            response.Data = role;
        }
    }
}