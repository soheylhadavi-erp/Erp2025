using Erp.Sharedkernel.Abstractions.Interfaces;
using System.Text.Json.Serialization;

namespace Erp.Sharedkernel.Common
{
    // در Erp.Sharedkernel.Common
    public abstract record DomainEvent : IDomainEvent
    {
        [JsonInclude]
        public Guid EventId { get; private set; } = Guid.NewGuid();

        [JsonInclude]
        public Guid AggregateId { get; init; }

        [JsonInclude]
        public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;

        [JsonInclude]
        public abstract string EventType { get; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        protected DomainEvent() { }
    }
}
