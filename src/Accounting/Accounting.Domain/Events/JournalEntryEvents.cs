using Accounting.Domain.ChartOfAccounts;
using Erp.Sharedkernel.Abstractions.Interfaces;
using Erp.Sharedkernel.Common;
namespace Accounting.Domain.JournalEntries
{
    public record JournalEntryCreatedEvent(JournalEntry JournalEntry) : DomainEvent
    {
        public override string EventType => nameof(JournalEntryCreatedEvent);
    }

    public record JournalItemAddedEvent(JournalEntry JournalEntry, JournalEntryItem Item) : DomainEvent
    {
        public override string EventType => nameof(JournalEntryCreatedEvent);
    }
    public record JournalPostedEvent(JournalEntry JournalEntry) : DomainEvent
    {
        public override string EventType => nameof(JournalEntryCreatedEvent);
    }
}
