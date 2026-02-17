using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(string memberId);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(string memberId);
    }
}