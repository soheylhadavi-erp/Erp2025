namespace Common.Domain
{
    internal class Audit : IAudit
    {
        public Guid? CreatorId { get; set; }
        public Guid? ModifierId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public Guid? TenantId { get; set; }
    }
}
