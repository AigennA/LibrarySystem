using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]
        [InlineData("Rowling", false)]
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            var result = book.Matches(searchTerm);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Sagan", true)]
        [InlineData("sagan", true)]
        [InlineData("Harry", false)]
        public void Book_Matches_ShouldFindByTitle(string searchTerm, bool expected)
        {
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            var result = book.Matches(searchTerm);

            Assert.Equal(expected, result);
        }
    }
}