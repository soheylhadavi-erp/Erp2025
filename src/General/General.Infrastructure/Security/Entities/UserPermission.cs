using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.Security.Entities
{
    public class UserPermission
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
