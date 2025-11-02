using Common.Domain.Interfaces.Base;

namespace Common.Domain.Entities.Base
{
    internal class Audit : IAudit
    {
        public Guid? CreatorId { get; set; }
        public Guid? ModifierId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public Guid? DeleteById { get; set; }
        public string ConcurrencyStamp { get; set; }= Guid.NewGuid().ToString();
        public Guid? TenantId { get; set; }
    }
}
