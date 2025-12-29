// Accounting.Domain/Exceptions/AccountingDomainException.cs
using System;

namespace Accounting.Domain.Exceptions
{
    /// <summary>
    /// Exception Related to errors of the domain (business rules) of accounting.
    /// </summary>
    public class AccountingDomainException : Exception
    {
        public AccountingDomainException() { }

        public AccountingDomainException(string message)
            : base(message) { }

        public AccountingDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}