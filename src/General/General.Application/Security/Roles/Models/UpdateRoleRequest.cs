using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Models
{
    public class UpdateRoleRequest
    {
        public Guid RoleId { get; set; }
        public string Description { get; set; }
    }
}
