namespace Common.Domain
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; }
    }
}
