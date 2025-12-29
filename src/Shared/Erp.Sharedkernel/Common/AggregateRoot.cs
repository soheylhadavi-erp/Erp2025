using Erp.Sharedkernel.Abstractions.Interfaces;
using Erp.Sharedkernel.Domain.Entities.Base;

namespace Erp.Sharedkernel.Common
{
    public abstract class AggregateRoot :
   BaseEntity,
   IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
