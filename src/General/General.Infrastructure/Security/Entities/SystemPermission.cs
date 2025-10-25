using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Security.Entities
{
    public class SystemPermission
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }        // "Users.Read"

        [MaxLength(200)]
        public string Description { get; set; } // "مشاهده کاربران"

        // 🔥 تغییر از string به Foreign Key
        public Guid CategoryId { get; set; }
        public virtual PermissionCategory Category { get; set; }

        // Navigation Properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
