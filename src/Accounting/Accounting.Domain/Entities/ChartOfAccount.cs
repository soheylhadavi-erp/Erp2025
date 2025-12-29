// Accounting.Domain/Entities/ChartOfAccount.cs
using Accounting.Domain.JournalEntries;
using Erp.Sharedkernel.Common;

namespace Accounting.Domain.ChartOfAccounts
{
    public class ChartOfAccount : AggregateRoot
    {
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string FullTitle { get; private set; }
        public AccountType AccountType { get; private set; }
        public AccountNature AccountNature { get; private set; }
        public int Level { get; private set; }
        public Guid? ParentId { get; private set; }
        public bool IsActive { get; private set; }
        public string Description { get; private set; }

        private readonly List<JournalEntryItem> _journalItems = new();
        public IReadOnlyCollection<JournalEntryItem> JournalItems => _journalItems.AsReadOnly();

        private ChartOfAccount() { }

        public ChartOfAccount(
            string code,
            string title,
            AccountType accountType,
            AccountNature accountNature,
            int level,
            Guid? parentId = null,
            string description = null)
        {
            Code = code;
            Title = title;
            AccountType = accountType;
            AccountNature = accountNature;
            Level = level;
            ParentId = parentId;
            IsActive = true;
            Description = description;

            GenerateFullTitle();
            AddDomainEvent(new ChartOfAccountCreatedEvent(this));
        }

        private void GenerateFullTitle()
        {
            FullTitle = $"{Code} - {Title}";
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
            GenerateFullTitle();

            AddDomainEvent(new ChartOfAccountUpdatedEvent(this));
        }
    }

    public enum AccountType
    {
        Asset = 1,
        Liability = 2,
        Equity = 3,
        Revenue = 4,
        Expense = 5
    }

    public enum AccountNature
    {
        Debit = 1,
        Credit = 2
    }
}