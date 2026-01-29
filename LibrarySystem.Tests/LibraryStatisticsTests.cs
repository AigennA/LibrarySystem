using Xunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBooks_ShouldReturnCorrectCount()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));
            library.AddBook(new Book("456", "Book2", "Author2", 2021));
            library.AddBook(new Book("789", "Book3", "Author3", 2022));

            var count = library.GetTotalBooks();

            Assert.Equal(3, count);
        }

        [Fact]
        public void GetBorrowedBooksCount_ShouldReturnCorrectCount()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));
            library.AddBook(new Book("456", "Book2", "Author2", 2021));
            library.AddMember(new Member("M001", "Anna", "anna@test.com"));

            library.BorrowBook("123", "M001");

            var count = library.GetBorrowedBooksCount();

            Assert.Equal(1, count);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));
            library.AddBook(new Book("456", "Book2", "Author2", 2021));
            library.AddBook(new Book("789", "Book3", "Author3", 2022));

            library.AddMember(new Member("M001", "Anna", "anna@test.com"));
            library.AddMember(new Member("M002", "Bob", "bob@test.com"));

            library.BorrowBook("123", "M001");
            library.BorrowBook("456", "M001");
            library.BorrowBook("789", "M002");

            var mostActive = library.GetMostActiveBorrower();

            Assert.NotNull(mostActive);
            Assert.Equal("M001", mostActive.MemberId);
        }

        [Fact]
        public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Zebra", "Author1", 2020));
            library.AddBook(new Book("456", "Alpha", "Author2", 2021));
            library.AddBook(new Book("789", "Beta", "Author3", 2022));

            var sorted = library.SortBooksByTitle();

            Assert.Equal("Alpha", sorted[0].Title);
            Assert.Equal("Beta", sorted[1].Title);
            Assert.Equal("Zebra", sorted[2].Title);
        }
    }
}