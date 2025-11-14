using _7Bank.Api.Models;

namespace _7Bank.Api.Repositories.TransactionRepository
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
        Task<IEnumerable<Transaction>> GetLastMonthsAsync(int accountId, int months);
        Task<bool> HasTransactionsAsync(int accountId);
        Task<IEnumerable<Transaction>> GetAllAsync();

    }
}
