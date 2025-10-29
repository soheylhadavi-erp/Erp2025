using General.Application.Auth.Permissions.Models;

namespace General.Application.Security.Roles.Models
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new();
    }
}
