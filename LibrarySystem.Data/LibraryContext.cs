using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=library.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.ISBN).IsRequired();
                entity.HasIndex(b => b.ISBN).IsUnique();
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
            });

            // Member configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MemberId);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Email).IsRequired().HasMaxLength(100);
            });

            // Loan configuration
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Member)
                    .WithMany(m => m.Loans)
                    .HasForeignKey(l => l.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
