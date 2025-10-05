using Microsoft.AspNetCore.Identity;
using General.Domain.Common;

namespace General.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<Guid>,AuditableIdentityEntity
{
    public string? Description { get; set; }
}
