using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Identity.Entities.Permissions
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
