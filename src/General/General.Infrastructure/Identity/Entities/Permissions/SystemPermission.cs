using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Identity.Entities.Permissions
{
    public class SystemPermission
    {
        public int Id { get; set; }
        public string Name { get; set; }        // Example: "Users.Read"
        public string Description { get; set; } // Example: "مشاهده کاربران"
        public string Category { get; set; }    // Example: "UserManagement"

        // Navigation Properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
