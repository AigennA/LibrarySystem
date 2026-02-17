using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Tests
{
    public class BookRepositoryTests : IDisposable
    {
        private readonly LibraryContext _context;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _repository = new BookRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldSaveBookToDatabase()
        {
            var book = new Book("978-0-1234-5678-9", "Test Book", "Test Author", 2024);

            await _repository.AddAsync(book);

            var books = await _context.Books.ToListAsync();
            Assert.Single(books);
            Assert.Equal("Test Book", books[0].Title);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            _context.Books.AddRange(
                new Book("978-0-0000-0001-0", "Book 1", "Author 1", 2020),
                new Book("978-0-0000-0002-0", "Book 2", "Author 2", 2021),
                new Book("978-0-0000-0003-0", "Book 3", "Author 3", 2022)
            );
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectBook()
        {
            var book = new Book("978-0-1234-5678-9", "Find Me", "Author", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(book.Id);

            Assert.NotNull(result);
            Assert.Equal("Find Me", result.Title);
        }

        [Fact]
        public async Task GetByISBNAsync_ShouldReturnCorrectBook()
        {
            var book = new Book("978-0-9999-8888-7", "ISBN Book", "Author", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByISBNAsync("978-0-9999-8888-7");

            Assert.NotNull(result);
            Assert.Equal("ISBN Book", result.Title);
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByTitle()
        {
            _context.Books.AddRange(
                new Book("978-0-0000-0001-0", "C# Programming", "Author 1", 2020),
                new Book("978-0-0000-0002-0", "Java Basics", "Author 2", 2021),
                new Book("978-0-0000-0003-0", "Advanced C#", "Author 3", 2022)
            );
            await _context.SaveChangesAsync();

            var result = await _repository.SearchAsync("C#");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyBook()
        {
            var book = new Book("978-0-1234-5678-9", "Old Title", "Author", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            book.Title = "New Title";
            await _repository.UpdateAsync(book);

            var updated = await _context.Books.FindAsync(book.Id);
            Assert.Equal("New Title", updated!.Title);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveBook()
        {
            var book = new Book("978-0-1234-5678-9", "Delete Me", "Author", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync(book.Id);

            var result = await _context.Books.FindAsync(book.Id);
            Assert.Null(result);
        }
    }
}
