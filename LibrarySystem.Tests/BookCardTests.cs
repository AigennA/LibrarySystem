using Bunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Web.Components.Shared;

namespace LibrarySystem.Tests
{
    public class BookCardTests : TestContext
    {
        [Fact]
        public void BookCard_ShouldDisplayBookTitle()
        {
            var book = new Book("978-0-1234-5678-9", "Test Title", "Test Author", 2024);

            var cut = RenderComponent<BookCard>(parameters => parameters
                .Add(p => p.Book, book));

            cut.Find(".card-title").MarkupMatches("<h5 class=\"card-title\">Test Title</h5>");
        }

        [Fact]
        public void BookCard_ShouldShowAvailableStatus()
        {
            var book = new Book("978-0-1234-5678-9", "Available Book", "Author", 2024)
            {
                IsAvailable = true
            };

            var cut = RenderComponent<BookCard>(parameters => parameters
                .Add(p => p.Book, book));

            var badge = cut.Find(".badge");
            Assert.Contains("bg-success", badge.ClassList);
            Assert.Equal("Tillgänglig", badge.TextContent);
        }

        [Fact]
        public void BookCard_ShouldShowBorrowedStatus()
        {
            var book = new Book("978-0-1234-5678-9", "Borrowed Book", "Author", 2024)
            {
                IsAvailable = false
            };

            var cut = RenderComponent<BookCard>(parameters => parameters
                .Add(p => p.Book, book));

            var badge = cut.Find(".badge");
            Assert.Contains("bg-danger", badge.ClassList);
            Assert.Equal("Utlånad", badge.TextContent);
        }
    }
}
