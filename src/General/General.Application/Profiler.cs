using AutoMapper;
using Common.Application;
using Common.Contract;
using General.Application.Auth.Permissions;
using General.Contract.Auth.Permissions;
using General.Contract.Roles;

namespace General.Application.Auth.Roles
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
