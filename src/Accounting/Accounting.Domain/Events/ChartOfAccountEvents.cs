using Erp.Sharedkernel.Abstractions.Interfaces;
using Erp.Sharedkernel.Common;
namespace Accounting.Domain.ChartOfAccounts
{
    public record ChartOfAccountCreatedEvent(ChartOfAccount Account) : DomainEvent
    {
        public override string EventType => nameof(ChartOfAccountCreatedEvent);
    }
    public record ChartOfAccountUpdatedEvent(ChartOfAccount Account) : DomainEvent
    {
        public override string EventType => nameof(ChartOfAccountCreatedEvent);
    }
}
