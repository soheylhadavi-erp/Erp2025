using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Roles.Models
{
    public class RoleOperationResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public RoleDto Role { get; set; }

        public static RoleOperationResult Success(string message = "Operation completed successfully", RoleDto role = null)
            => new() { Succeeded = true, Message = message, Role = role };

        public static RoleOperationResult Failure(string error)
            => new() { Succeeded = false, Errors = new() { error } };

        public static RoleOperationResult Failure(List<string> errors)
            => new() { Succeeded = false, Errors = errors };
    }
}
