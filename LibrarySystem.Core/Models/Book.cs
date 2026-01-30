using System;

namespace LibrarySystem.Core.Models
{
    public class Book : ISearchable
    {
        public string ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public int PublishedYear { get; }
        public bool IsAvailable { get; set; }

        public Book(string isbn, string title, string author, int publishedYear)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN cannot be empty", nameof(isbn));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty", nameof(author));

            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        public string GetInfo()
        {
            string availability = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $"\"{Title}\" av {Author} ({PublishedYear}) - ISBN: {ISBN} - {availability}";
        }

        public bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();
            return Title.ToLower().Contains(searchTerm) ||
                   Author.ToLower().Contains(searchTerm) ||
                   ISBN.ToLower().Contains(searchTerm);
        }
    }
}
