namespace General.Application.Auth.Permissions.Models
{
    public class PermissionCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new();
    }
}
