using AutoMapper;
using Common.Contract;
using General.Application.Auth.Roles;
using General.Contract.Roles;
using MediatR;
namespace General.Application.Security.Roles
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuey, ApiResponse<RoleResponse>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RoleResponse>> Handle(GetRoleByIdQuey request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRoleByIdAsync(request.RoleId);
            var role = _mapper.Map<RoleDto, RoleResponse>(result);
            if (role != null)
            {
                return new ApiResponse<RoleResponse>()
                { Succeeded = true, Message = "Role Received Successfully", Data = role };
            }

            return new ApiResponse<RoleResponse>()
            { Succeeded = false, Message = "Role Not Found", Data = null };
        }
    }
}