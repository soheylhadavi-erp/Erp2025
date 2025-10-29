using Common.Contract.Responses;
using General.Application.Security.Roles.Interfaces;
using General.Application.Security.Roles.Models;
using General.Contract.Roles.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : ICreateRoleInput,IRequest<ApiResponse<CreateRoleResponse>>
    {
    }
}
