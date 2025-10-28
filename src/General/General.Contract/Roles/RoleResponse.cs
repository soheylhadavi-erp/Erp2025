using General.Contract.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Contract.Roles
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionResponse> Permissions { get; set; } = new();
    }
}
