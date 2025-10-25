using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Security.Entities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
