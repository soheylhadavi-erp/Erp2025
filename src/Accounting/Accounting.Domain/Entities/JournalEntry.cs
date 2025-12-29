// Accounting.Domain/Entities/JournalEntry.cs
using Accounting.Domain.Exceptions;
using Erp.Sharedkernel.Common;
using Erp.Sharedkernel.Domain.Entities.Base;

namespace Accounting.Domain.JournalEntries
{
    public class JournalEntry : AggregateRoot
    {
        public string VoucherNumber { get; private set; }
        public DateTime VoucherDate { get; private set; }
        public string Description { get; private set; }
        public decimal TotalDebit { get; private set; }
        public decimal TotalCredit { get; private set; }
        public bool IsPosted { get; private set; }
        public DateTime? PostingDate { get; private set; }
        public Guid? CreatedByUserId { get; private set; }

        private readonly List<JournalEntryItem> _items = new();
        public IReadOnlyCollection<JournalEntryItem> Items => _items.AsReadOnly();

        private JournalEntry() { }

        public JournalEntry(
            string voucherNumber,
            DateTime voucherDate,
            string description,
            Guid? createdByUserId = null)
        {
            VoucherNumber = voucherNumber;
            VoucherDate = voucherDate;
            Description = description;
            CreatedByUserId = createdByUserId;
            IsPosted = false;

            AddDomainEvent(new JournalEntryCreatedEvent(this));
        }

        public void AddItem(
            Guid accountId,
            string description,
            decimal debit,
            decimal credit)
        {
            var item = new JournalEntryItem(
                accountId,
                description,
                debit,
                credit,
                Id);

            _items.Add(item);
            RecalculateTotals();

            AddDomainEvent(new JournalItemAddedEvent(this, item));
        }

        public void PostJournal()
        {
            if (Math.Abs(TotalDebit - TotalCredit) > 0.01m)
                throw new AccountingDomainException("Debit and Credit must be equal");

            IsPosted = true;
            PostingDate = DateTime.Now;

            AddDomainEvent(new JournalPostedEvent(this));
        }

        private void RecalculateTotals()
        {
            TotalDebit = _items.Sum(x => x.Debit);
            TotalCredit = _items.Sum(x => x.Credit);
        }
    }

    public class JournalEntryItem : BaseEntity
    {
        public Guid AccountId { get; private set; }
        public string Description { get; private set; }
        public decimal Debit { get; private set; }
        public decimal Credit { get; private set; }
        public Guid JournalEntryId { get; private set; }

        private JournalEntryItem() { }

        public JournalEntryItem(
            Guid accountId,
            string description,
            decimal debit,
            decimal credit,
            Guid journalEntryId)
        {
            AccountId = accountId;
            Description = description;
            Debit = debit;
            Credit = credit;
            JournalEntryId = journalEntryId;
        }
    }
}