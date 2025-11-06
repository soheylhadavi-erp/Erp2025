using General.Infrastructure.Auth.Permissions;
using General.Infrastructure.Auth.Roles;
using General.Infrastructure.Auth.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Auth.permissions
{
    public class SystemPermission
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }        // "Users.Read"

        [MaxLength(200)]
        public string Description { get; set; } // "مشاهده کاربران"

        
        public Guid CategoryId { get; set; }
        public virtual PermissionCategory Category { get; set; }

        // Navigation Properties
        public virtual ICollection<ApplicationRole> Roles { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
