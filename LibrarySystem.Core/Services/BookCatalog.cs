using System;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Services
{
    public class BookCatalog
    {
        private List<Book> _books = new();

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (_books.Any(b => b.ISBN == book.ISBN))
                throw new ArgumentException("A book with this ISBN already exists");

            _books.Add(book);
        }

        public List<Book> GetAllBooks() => new List<Book>(_books);

        public Book? FindBookByISBN(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _books.Where(b => b.Matches(searchTerm)).ToList();
        }

        public List<Book> SortBooksByTitle()
        {
            return _books.OrderBy(b => b.Title).ToList();
        }

        public List<Book> SortBooksByYear()
        {
            return _books.OrderBy(b => b.PublishedYear).ToList();
        }

        public int GetTotalBooks() => _books.Count;
    }
}