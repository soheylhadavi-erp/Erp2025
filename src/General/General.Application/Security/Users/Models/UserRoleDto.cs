using General.Application.Auth.Permissions.Models;

namespace General.Application.Auth.Users.Models
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<PermissionDto> DirectPermissions { get; set; } = new();
    }
}
