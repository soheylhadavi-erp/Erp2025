using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Models
{
    public class RemoveUserFromRoleRequest
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
