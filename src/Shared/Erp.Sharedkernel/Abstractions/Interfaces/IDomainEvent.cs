namespace Erp.Sharedkernel.Abstractions.Interfaces
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        Guid AggregateId { get; }
        DateTime OccurredOn { get; }
        string EventType { get; }
    }
}
