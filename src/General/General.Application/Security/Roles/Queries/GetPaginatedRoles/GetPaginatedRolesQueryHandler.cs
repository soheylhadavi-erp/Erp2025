using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using General.Application.Security.Roles.Models;
using General.Contract.Roles;
using MediatR;
namespace General.Application.Security.Roles
{
    public class GetPaginatedRolesQueryHandler : IRequestHandler<GetPaginatedRolesQuery, ApiResponse<PagedResponse<RoleResponse>>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetPaginatedRolesQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResponse<RoleResponse>>> Handle(GetPaginatedRolesQuery request, CancellationToken cancellationToken)
        {
            var serviceRequest = _mapper.Map<GetPaginatedRolesQuery, PaginatedRequest>(request);
            var result = await _roleService.GetPaginatedRolesAsync(serviceRequest);
            var roles = _mapper.Map<PaginatedResultDto<RoleDto>, PagedResponse<RoleResponse>>(result);
            var response = new ApiResponse<PagedResponse<RoleResponse>>()
            { Succeeded = true, Message = "Roles Received Successfully ", Data = roles };
            return response;
        }
    }
}