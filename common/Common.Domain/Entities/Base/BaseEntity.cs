using Common.Domain.Interfaces.Base;

namespace Common.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; }
    }
}
