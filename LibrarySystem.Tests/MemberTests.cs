using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class MemberTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var member = new Member("M001", "Test Person", "test@email.com");

            Assert.Equal("M001", member.MemberId);
            Assert.Equal("Test Person", member.Name);
            Assert.Equal("test@email.com", member.Email);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenMemberIdIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Member("", "Name", "email@test.com"));
        }

        [Fact]
        public void GetActiveLoans_ShouldReturnOnlyNonReturnedLoans()
        {
            var member = new Member("M001", "Test", "test@test.com");
            var book1 = new Book("123", "Book1", "Author1", 2020);
            var book2 = new Book("456", "Book2", "Author2", 2021);

            var loan1 = new Loan(book1, member, DateTime.Now, DateTime.Now.AddDays(14));
            var loan2 = new Loan(book2, member, DateTime.Now, DateTime.Now.AddDays(14));

            loan1.ReturnBook();
            member.AddLoan(loan1);
            member.AddLoan(loan2);

            var activeLoans = member.GetActiveLoans().ToList();

            Assert.Single(activeLoans);
            Assert.Equal("Book2", activeLoans[0].Book.Title);
        }

        [Theory]
        [InlineData("Test", true)]
        [InlineData("test", true)]
        [InlineData("M001", true)]
        [InlineData("NonExistent", false)]
        public void Matches_ShouldFindMemberBySearchTerm(string searchTerm, bool expected)
        {
            var member = new Member("M001", "Test Person", "test@email.com");

            var result = member.Matches(searchTerm);

            Assert.Equal(expected, result);
        }
    }
}