using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Tests
{
    public class LoanRepositoryTests : IDisposable
    {
        private readonly LibraryContext _context;
        private readonly LoanRepository _repository;

        public LoanRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _repository = new LoanRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task<(Book book, Member member)> SeedBookAndMember()
        {
            var book = new Book("978-0-1234-5678-9", "Test Book", "Author", 2024);
            var member = new Member("M001", "Test Member", "test@example.com");
            _context.Books.Add(book);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return (book, member);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveLoanToDatabase()
        {
            var (book, member) = await SeedBookAndMember();
            var loan = new Loan
            {
                BookId = book.Id,
                MemberId = member.MemberId,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            await _repository.AddAsync(loan);

            var loans = await _context.Loans.ToListAsync();
            Assert.Single(loans);
            Assert.Equal(book.Id, loans[0].BookId);
        }

        [Fact]
        public async Task GetActiveLoansAsync_ShouldReturnOnlyActiveLoans()
        {
            var (book, member) = await SeedBookAndMember();
            var book2 = new Book("978-0-0000-0002-0", "Book 2", "Author 2", 2021);
            _context.Books.Add(book2);
            await _context.SaveChangesAsync();

            _context.Loans.AddRange(
                new Loan
                {
                    BookId = book.Id,
                    MemberId = member.MemberId,
                    LoanDate = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now.AddDays(4),
                    ReturnDate = null // active
                },
                new Loan
                {
                    BookId = book2.Id,
                    MemberId = member.MemberId,
                    LoanDate = DateTime.Now.AddDays(-20),
                    DueDate = DateTime.Now.AddDays(-6),
                    ReturnDate = DateTime.Now.AddDays(-5) // returned
                }
            );
            await _context.SaveChangesAsync();

            var result = await _repository.GetActiveLoansAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetOverdueLoansAsync_ShouldReturnOverdueLoans()
        {
            var (book, member) = await SeedBookAndMember();
            var book2 = new Book("978-0-0000-0002-0", "Book 2", "Author 2", 2021);
            _context.Books.Add(book2);
            await _context.SaveChangesAsync();

            _context.Loans.AddRange(
                new Loan
                {
                    BookId = book.Id,
                    MemberId = member.MemberId,
                    LoanDate = DateTime.Now.AddDays(-30),
                    DueDate = DateTime.Now.AddDays(-10), // overdue
                    ReturnDate = null
                },
                new Loan
                {
                    BookId = book2.Id,
                    MemberId = member.MemberId,
                    LoanDate = DateTime.Now.AddDays(-5),
                    DueDate = DateTime.Now.AddDays(9), // not overdue
                    ReturnDate = null
                }
            );
            await _context.SaveChangesAsync();

            var result = await _repository.GetOverdueLoansAsync();

            Assert.Single(result);
            Assert.Equal(book.Id, result.First().BookId);
        }
    }
}
