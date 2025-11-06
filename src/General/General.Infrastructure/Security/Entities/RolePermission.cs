using General.Infrastructure.Auth.permissions;
using General.Infrastructure.Auth.Roles;

namespace General.Infrastructure.Security.Entities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
