using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class LoanEdgeCaseTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenBookIsNull()
        {
            var member = new Member("M001", "Test", "test@test.com");

            Assert.Throws<ArgumentNullException>(() =>
                new Loan(null!, member, DateTime.Now, DateTime.Now.AddDays(14)));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenMemberIsNull()
        {
            var book = new Book("123", "Test", "Author", 2024);

            Assert.Throws<ArgumentNullException>(() =>
                new Loan(book, null!, DateTime.Now, DateTime.Now.AddDays(14)));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDueDateBeforeLoanDate()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");

            Assert.Throws<ArgumentException>(() =>
                new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(-1)));
        }

        [Fact]
        public void GetDaysOverdue_ShouldReturnZero_WhenNotOverdue()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            var days = loan.GetDaysOverdue();

            Assert.Equal(0, days);
        }

        [Fact]
        public void GetDaysOverdue_ShouldReturnCorrectDays_WhenOverdue()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));

            var days = loan.GetDaysOverdue();

            Assert.True(days >= 5);
        }

        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenBookIsReturned()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));

            loan.ReturnBook();

            Assert.False(loan.IsOverdue);
        }
    }
}