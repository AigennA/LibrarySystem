using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core.Models
{
    public class Loan
    {
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public bool IsReturned => ReturnDate.HasValue;

        private const decimal FeePerDay = 10m;

        public Loan()
        {
            Book = null!;
            Member = null!;
            LoanDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(14);
        }

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (dueDate <= loanDate)
                throw new ArgumentException("Due date must be after loan date");

            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = dueDate;
        }

        public void ReturnBook()
        {
            if (IsReturned)
                throw new InvalidOperationException("Book has already been returned");

            ReturnDate = DateTime.Now;
            Book.IsAvailable = true;
        }

        public int GetDaysOverdue()
        {
            if (!IsOverdue)
                return 0;

            return (DateTime.Now - DueDate).Days;
        }

        public decimal CalculateLateFee()
        {
            if (!IsOverdue)
                return 0m;

            return GetDaysOverdue() * FeePerDay;
        }

        public string GetLoanSummary()
        {
            var status = IsReturned ? "Returnerad" : (IsOverdue ? "Försenad" : "Aktiv");
            var summary = $"Lån: \"{Book?.Title ?? "Unknown"}\" - Status: {status}";

            if (IsOverdue && !IsReturned)
            {
                summary += $" ({GetDaysOverdue()} dagar försenad, avgift: {CalculateLateFee():C})";
            }

            return summary;
        }
    }
}