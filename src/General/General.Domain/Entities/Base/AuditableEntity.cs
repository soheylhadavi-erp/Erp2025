using System;
using System.ComponentModel.DataAnnotations.Schema;
using ErpProject.General.Entities.Base;

namespace General.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public Guid? CreatorId { get; set; }
        public Guid? ModifierId { get; set; }
        
        //[ForeignKey(nameof(CreatorId))]
        //public virtual ApplicationUser? Creator { get; set; }
        
        //[ForeignKey(nameof(ModifierId))]
        //public virtual ApplicationUser? Modifier { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public Guid? DeleteById { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public Guid? TenantId { get; set; }
    }
}
