using General.Infrastructure.Auth.permissions;
using System.ComponentModel.DataAnnotations;

namespace General.Infrastructure.Auth.Permissions
{
    public class PermissionCategory
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }        // "UserManagement"

        [MaxLength(200)]
        public string Description { get; set; } // "مدیریت کاربران"

        public int DisplayOrder { get; set; }   // ترتیب نمایش

        // Navigation Properties
        public virtual ICollection<SystemPermission> Permissions { get; set; }
    }
}
