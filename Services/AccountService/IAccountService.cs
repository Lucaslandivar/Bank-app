using _7Bank.Api.DTOs;
using _7Bank.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _7Bank.Api.Services.AccountService
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(int userId);

        Task<Account?> GetByIdAsync(int accountId);
        Task<Account?> GetByUserIdAsync(int userId);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<decimal> GetBalanceAsync(int accountId);
        Task<Account?> UpdateAccountAsync(AccountUpdateDto dto);
        Task<bool> InactiveAccountAsync(int accountId);
        Task<bool> DeleteAccountAsync(int accountId);

        Task<int> GetAccountsCreated3MonthsAsync();
    }
}
