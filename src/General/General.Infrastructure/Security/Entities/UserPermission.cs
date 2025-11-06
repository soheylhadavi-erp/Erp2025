using General.Infrastructure.Auth.permissions;
using General.Infrastructure.Auth.Users;

namespace General.Infrastructure.Auth.Permissions
{
    public class UserPermission
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
