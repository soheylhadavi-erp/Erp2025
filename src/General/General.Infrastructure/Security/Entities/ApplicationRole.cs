using General.Infrastructure.Auth.permissions;
using General.Infrastructure.Auth.Permissions;
using Microsoft.AspNetCore.Identity;

namespace General.Infrastructure.Auth.Roles;

public class ApplicationRole : IdentityRole<Guid>//,AuditableIdentityEntity
{
    public string? Description { get; set; }
    public virtual ICollection<SystemPermission> Permissions { get; set; }
}
