using General.Application.Auth.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Auth.Roles.Models
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new();
    }
}
