using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Interfaces
{
    public class ICreateRoleInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
