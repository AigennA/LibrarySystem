using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core.Models
{
    public class Member : ISearchable
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MemberSince { get; set; }

        private List<Loan> _loans = new();
        public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

        public Member()
        {
            MemberId = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            MemberSince = DateTime.Now;
        }

        public Member(string memberId, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("Member ID cannot be empty", nameof(memberId));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = DateTime.Now;
        }

        public void AddLoan(Loan loan)
        {
            _loans.Add(loan);
        }

        public void RemoveLoan(Loan loan)
        {
            _loans.Remove(loan);
        }

        public IEnumerable<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned);
        }

        public string GetMemberInfo()
        {
            return $"Medlem: {Name} (ID: {MemberId})\nE-post: {Email}\nMedlem sedan: {MemberSince:yyyy-MM-dd}\nAktiva lån: {GetActiveLoans().Count()}";
        }

        public bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();
            return Name.ToLower().Contains(searchTerm) ||
                   MemberId.ToLower().Contains(searchTerm) ||
                   Email.ToLower().Contains(searchTerm);
        }
    }
}
