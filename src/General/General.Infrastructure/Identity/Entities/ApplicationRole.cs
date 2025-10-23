using Microsoft.AspNetCore.Identity;
using General.Domain.Common;

namespace General.Infrastructure.Identity.Entities;

public class ApplicationRole : IdentityRole<Guid>//,AuditableIdentityEntity
{
    public string? Description { get; set; }
}
