using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces.Base
{
    public interface IAudit
    {
        public Guid? CreatorId { get; set; }
        public Guid? ModifierId { get; set; }

        //[ForeignKey(nameof(CreatorId))]
        //public virtual ApplicationUser? Creator { get; set; }

        //[ForeignKey(nameof(ModifierId))]
        //public virtual ApplicationUser? Modifier { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public Guid? DeletedById { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public string ConcurrencyStamp { get; set; }/* { get; set; } = Guid.NewGuid().ToString();*/
        public Guid? TenantId { get; set; }
    }
}
