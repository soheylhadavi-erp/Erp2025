namespace General.Application.Auth.Permissions
{

    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsAssigned { get; set; } // برای UI - آیا اختصاص داده شده
        public bool IsDirect { get; set; }
    }
}
