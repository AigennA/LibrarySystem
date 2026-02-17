using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            var book = new Book("123", "Test", "Author", 2024);

            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            var book = new Book("123", "Testbok", "Testförfattare", 2024);

            var info = book.GetInfo();

            Assert.Contains("Testbok", info);
            Assert.Contains("Testförfattare", info);
            Assert.Contains("2024", info);
            Assert.Contains("Tillgänglig", info);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenISBNIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Book("", "Title", "Author", 2024));
        }
    }
}