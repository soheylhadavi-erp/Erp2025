﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Models
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
