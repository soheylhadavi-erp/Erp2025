using Common.Domain;
using General.Infrastructure.Auth.permissions;
using Microsoft.AspNetCore.Identity;

namespace General.Infrastructure.Auth.Users;

public class ApplicationUser : IdentityUser<Guid>, IAudit, ISoftDelete
{
    public string? FullName { get; set; }
    public string? NationalCode { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<SystemPermission> Permissions { get; set; }
    //Implemented ISoftDelete fields
    public bool IsDeleted { get; set; }
    //Implemented IAudit fields
    public Guid? CreatorId { get; set; }
    public Guid? ModifierId { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? ModifyDateTime { get; set; }
    public Guid? DeletedById { get; set; }
    public DateTime? DeleteDateTime { get; set; }
    override public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public Guid? TenantId { get; set; }
}
