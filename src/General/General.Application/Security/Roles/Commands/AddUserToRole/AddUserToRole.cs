using General.Application.Security.Roles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Commands.AddUserToRole
{
    public class AddUserToRoleCommand : IRequest<RoleOperationResult>
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
