using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryContext _context;

        public MemberRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(string memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string memberId)
        {
            var member = await GetByIdAsync(memberId);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}