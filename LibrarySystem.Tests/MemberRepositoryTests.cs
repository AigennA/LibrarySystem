using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Tests
{
    public class MemberRepositoryTests : IDisposable
    {
        private readonly LibraryContext _context;
        private readonly MemberRepository _repository;

        public MemberRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new LibraryContext(options);
            _repository = new MemberRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ShouldSaveMemberToDatabase()
        {
            var member = new Member("M001", "Anna Svensson", "anna@example.com");

            await _repository.AddAsync(member);

            var members = await _context.Members.ToListAsync();
            Assert.Single(members);
            Assert.Equal("Anna Svensson", members[0].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectMember()
        {
            var member = new Member("M002", "Erik Johansson", "erik@example.com");
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync("M002");

            Assert.NotNull(result);
            Assert.Equal("Erik Johansson", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveMember()
        {
            var member = new Member("M003", "Lisa Nilsson", "lisa@example.com");
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync("M003");

            var result = await _context.Members.FindAsync("M003");
            Assert.Null(result);
        }
    }
}
