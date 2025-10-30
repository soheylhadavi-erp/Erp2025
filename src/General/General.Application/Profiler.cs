using AutoMapper;
using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Auth.Permissions.Models;
using General.Application.Auth.Users.Models;
using General.Application.Security.Roles.Models;
using General.Contract.Permissions;
using General.Contract.Roles;

namespace General.Application.Security.Roles
{
    public class Profiler : Profile
    {
        public Profiler()
        {
            // Roles
            CreateMap<RoleDto, RoleResponse>();
            CreateMap<OperationResultDto<RoleDto>, ApiResponse<RoleResponse>>();
            CreateMap<PermissionDto, PermissionResponse>();

            CreateMap<OperationResultDto, ApiResponse>();
            CreateMap<GetPaginatedRolesQuery, PaginatedRequest>();
            CreateMap<PaginatedResultDto<RoleDto>, PagedResponse<RoleResponse>>();


        }
    }
}
