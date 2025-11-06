using General.Contract.Auth.Permissions;

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
