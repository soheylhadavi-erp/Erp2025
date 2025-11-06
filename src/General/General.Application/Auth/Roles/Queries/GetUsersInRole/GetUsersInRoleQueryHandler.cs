using AutoMapper;
using Common.Contract;
using General.Application.Auth.Roles;
using General.Application.Auth.Users;
using General.Contract.Auth.Users;
using MediatR;
namespace General.Application.Security.Roles
{
    public class GetUsersInRoleQueryHandler : IRequestHandler<GetUsersInRoleQuery, ApiResponse<List<UserResponse>>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetUsersInRoleQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetUsersInRoleAsync(request.RoleId);
            var users = _mapper.Map<List<UserDto>, List<UserResponse>>(result);
            var response = new ApiResponse<List<UserResponse>>()
            { Succeeded = true, Message = $"Users In Role by Id : {request.RoleId} Received Successfully ", Data = users };
            return response;
        }
    }
}