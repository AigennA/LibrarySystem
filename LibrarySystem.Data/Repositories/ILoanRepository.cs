using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task<Loan?> GetActiveLoanByBookIdAsync(int bookId);
    }
}