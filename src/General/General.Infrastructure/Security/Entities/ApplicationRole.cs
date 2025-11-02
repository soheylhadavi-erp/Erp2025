using Microsoft.AspNetCore.Identity;

namespace General.Infrastructure.Security.Entities;

public class ApplicationRole : IdentityRole<Guid>//,AuditableIdentityEntity
{
    public string? Description { get; set; }
    public virtual ICollection<SystemPermission> Permissions { get; set; }
}
