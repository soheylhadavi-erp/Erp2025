using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using General.Domain.Common;

namespace General.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>//, AuditableIdentityEntity
{
    public string? FullName { get; set; }
    public string? NationalCode { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
 
}
