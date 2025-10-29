using General.Contract.Permissions;

namespace General.Contract.Roles
{
    public class GetRoleByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionResponse> Permissions { get; set; } = new();
    }
}
