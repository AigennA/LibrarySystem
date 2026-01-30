using System;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services
{
    public class LoanManager
    {
        private List<Loan> _loans = new();

        public Loan CreateLoan(Book book, Member member)
        {
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(Loan.DefaultLoanPeriodDays));
            _loans.Add(loan);
            return loan;
        }

        public Loan? FindActiveLoanByISBN(string isbn)
        {
            return _loans.FirstOrDefault(l => l.Book.ISBN == isbn && !l.IsReturned);
        }

        public int GetActiveLoanCount()
        {
            return _loans.Count(l => !l.IsReturned);
        }

        public Member? GetMostActiveBorrower()
        {
            if (!_loans.Any())
                return null;

            return _loans
                .GroupBy(l => l.Member)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key;
        }

        public List<Loan> GetOverdueLoans()
        {
            return _loans.Where(l => l.IsOverdue).ToList();
        }
    }
}