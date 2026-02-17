using Xunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void AddBook_ShouldThrowException_WhenDuplicateISBN()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));

            Assert.Throws<ArgumentException>(() =>
                library.AddBook(new Book("123", "Book2", "Author2", 2021)));
        }

        [Fact]
        public void AddMember_ShouldThrowException_WhenDuplicateMemberId()
        {
            var library = new Library();
            library.AddMember(new Member("M001", "Person1", "email1@test.com"));

            Assert.Throws<ArgumentException>(() =>
                library.AddMember(new Member("M001", "Person2", "email2@test.com")));
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenBookNotFound()
        {
            var library = new Library();
            library.AddMember(new Member("M001", "Test", "test@test.com"));

            Assert.Throws<ArgumentException>(() =>
                library.BorrowBook("999", "M001"));
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenMemberNotFound()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));

            Assert.Throws<ArgumentException>(() =>
                library.BorrowBook("123", "M999"));
        }

        [Fact]
        public void BorrowBook_ShouldThrowException_WhenBookNotAvailable()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));
            library.AddMember(new Member("M001", "Test1", "test1@test.com"));
            library.AddMember(new Member("M002", "Test2", "test2@test.com"));

            library.BorrowBook("123", "M001");

            Assert.Throws<InvalidOperationException>(() =>
                library.BorrowBook("123", "M002"));
        }

        [Fact]
        public void ReturnBook_ShouldThrowException_WhenNoActiveLoan()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));

            Assert.Throws<ArgumentException>(() =>
                library.ReturnBook("123"));
        }

        [Fact]
        public void SearchBooks_ShouldReturnEmptyList_WhenNoMatches()
        {
            var library = new Library();
            library.AddBook(new Book("123", "Book1", "Author1", 2020));

            var results = library.SearchBooks("NonExistent");

            Assert.Empty(results);
        }

        [Fact]
        public void SortBooksByYear_ShouldReturnCorrectOrder()
        {
            var library = new Library();
            library.AddBook(new Book("123", "NewBook", "Author1", 2022));
            library.AddBook(new Book("456", "OldBook", "Author2", 2010));
            library.AddBook(new Book("789", "MidBook", "Author3", 2015));

            var sorted = library.SortBooksByYear();

            Assert.Equal(2010, sorted[0].PublishedYear);
            Assert.Equal(2015, sorted[1].PublishedYear);
            Assert.Equal(2022, sorted[2].PublishedYear);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnNull_WhenNoLoans()
        {
            var library = new Library();

            var result = library.GetMostActiveBorrower();

            Assert.Null(result);
        }
    }
}