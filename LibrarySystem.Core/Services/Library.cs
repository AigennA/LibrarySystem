using System;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services
{
    // Facade-klass som använder komposition för att samordna delsystemen
    public class Library
    {
        private readonly BookCatalog _bookCatalog;
        private readonly MemberRegistry _memberRegistry;
        private readonly LoanManager _loanManager;

        public Library()
        {
            _bookCatalog = new BookCatalog();
            _memberRegistry = new MemberRegistry();
            _loanManager = new LoanManager();
        }

        public void AddBook(Book book) => _bookCatalog.AddBook(book);
        public List<Book> GetAllBooks() => _bookCatalog.GetAllBooks();
        public Book? FindBookByISBN(string isbn) => _bookCatalog.FindBookByISBN(isbn);
        public List<Book> SearchBooks(string searchTerm) => _bookCatalog.SearchBooks(searchTerm);
        public List<Book> SortBooksByTitle() => _bookCatalog.SortBooksByTitle();
        public List<Book> SortBooksByYear() => _bookCatalog.SortBooksByYear();

        public void AddMember(Member member) => _memberRegistry.AddMember(member);
        public List<Member> GetAllMembers() => _memberRegistry.GetAllMembers();
        public Member? FindMemberById(string memberId) => _memberRegistry.FindMemberById(memberId);

        public Loan BorrowBook(string isbn, string memberId)
        {
            var book = FindBookByISBN(isbn);
            var member = FindMemberById(memberId);

            if (book == null)
                throw new ArgumentException("Book not found");
            if (member == null)
                throw new ArgumentException("Member not found");
            if (!book.IsAvailable)
                throw new InvalidOperationException("Book is not available");

            var loan = _loanManager.CreateLoan(book, member);
            book.IsAvailable = false;
            member.AddLoan(loan);

            return loan;
        }

        public void ReturnBook(string isbn)
        {
            var loan = _loanManager.FindActiveLoanByISBN(isbn);
            if (loan == null)
                throw new ArgumentException("No active loan found for this book");

            loan.ReturnBook();
            // Lånet finns kvar i medlemmens historik (tas inte bort)
        }

        public int GetTotalBooks() => _bookCatalog.GetTotalBooks();
        public int GetBorrowedBooksCount() => _loanManager.GetActiveLoanCount();
        public Member? GetMostActiveBorrower() => _loanManager.GetMostActiveBorrower();
        public List<Loan> GetOverdueLoans() => _loanManager.GetOverdueLoans();
    }
}