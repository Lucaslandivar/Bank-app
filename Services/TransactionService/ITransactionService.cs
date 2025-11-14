using _7Bank.Api.DTOs.Transaction;
using _7Bank.Api.Models;

namespace _7Bank.Api.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> TransferAsync(
            int fromUserId,
            string identifier,
            string identifierType,
            decimal amount
        );

        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Transaction>> GetLast3MonthsAsync(int userId);
    }
}

