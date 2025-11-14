using _7Bank.Api.Models;

namespace _7Bank.Api.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task<Account?> GetByIdAsync(int id);
        Task<Account?> GetByUserIdAsync(int  userId);
        Task<Account?> GetByAccountNumberAsync(string accountNumber);
        Task<IEnumerable<Account>> GetAllAsync();

        Task<bool> DeleteAccountAsync(int accountId);
        Task<bool> InactiveAccountAsync(int accountId);
        Task<bool> HaveTransactionsAsync(int accountId);
    }
}
