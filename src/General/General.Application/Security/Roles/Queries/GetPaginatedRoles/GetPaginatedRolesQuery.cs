using Common.Application.Models;
using Common.Contract.Responses;
using General.Application.Security.Roles.Models;
using General.Contract.Roles;
using MediatR;

namespace General.Application.Security.Roles
{
    public class GetPaginatedRolesQuery : IRequest<ApiResponse<PagedResponse<RoleResponse>>> 
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SkipRecords { get; set; } = 0; // اضافه شدن Skip

        public const int MaxPageSize = 100;
        public const int DefaultPageSize = 10;
    }
}
