using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));

            Assert.True(loan.IsOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            loan.ReturnBook();

            Assert.True(loan.IsReturned);
        }

        [Fact]
        public void ReturnBook_ShouldSetBookAsAvailable()
        {
            var book = new Book("123", "Test", "Author", 2024);
            book.IsAvailable = false;
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            loan.ReturnBook();

            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void ReturnBook_ShouldThrowException_WhenAlreadyReturned()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            loan.ReturnBook();

            Assert.Throws<InvalidOperationException>(() => loan.ReturnBook());
        }
    }
}