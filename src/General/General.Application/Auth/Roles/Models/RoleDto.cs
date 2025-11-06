using General.Application.Auth.Permissions;

namespace General.Application.Auth.Roles
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new();
    }
}
