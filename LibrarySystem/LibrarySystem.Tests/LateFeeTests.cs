using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class LateFeeTests
    {
        [Fact]
        public void CalculateLateFee_ShouldReturnZero_WhenNotOverdue()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            var fee = loan.CalculateLateFee();

            Assert.Equal(0m, fee);
        }

        [Fact]
        public void CalculateLateFee_ShouldReturnCorrectAmount_WhenOverdue()
        {
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-10));

            var fee = loan.CalculateLateFee();

            Assert.True(fee >= 100m);
        }

        [Fact]
        public void GetLoanSummary_ShouldIncludeLateFee_WhenOverdue()
        {
            var book = new Book("123", "Test Book", "Author", 2024);
            var member = new Member("M001", "Test", "test@test.com");
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));

            var summary = loan.GetLoanSummary();

            Assert.Contains("Försenad", summary);
            Assert.Contains("avgift", summary);
        }
    }
}