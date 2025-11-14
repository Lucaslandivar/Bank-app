using _7Bank.Api.Data;
using _7Bank.Api.DTOs;
using _7Bank.Api.Models;
using _7Bank.Api.Repositories.AccountRepository;
using Microsoft.EntityFrameworkCore;

namespace _7Bank.Api.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly BankDbContext _context;

        public AccountService(IAccountRepository accountRepository, BankDbContext context)
        {
            _accountRepository = accountRepository;
            _context = context;
        }

        private string GenerateAccountNumber() => new Random().Next(100000, 999999).ToString();
        public async Task<Account> CreateAccountAsync(int userId)
        {
            var account = new Account
            {
                UserId = userId,
                AccountNumber = GenerateAccountNumber(),
                Balance = 0
            };

            return await _accountRepository.CreateAccountAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int accountId) => await _accountRepository.DeleteAccountAsync(accountId);

        public async Task<IEnumerable<Account>> GetAllAsync() => await _accountRepository.GetAllAsync();

        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new Exception("Conta não encontrada!");
            return account.Balance;
        }

        public async Task<Account?> GetByIdAsync(int accountId) => await _accountRepository.GetByIdAsync(accountId);

        public async Task<Account?> GetByUserIdAsync(int userId) => await _accountRepository.GetByUserIdAsync(userId);

        public async Task<int> GetAccountsCreated3MonthsAsync()
        {
            var Top3Accounts = DateTime.Now.AddMonths(-3);

            return await _context.Accounts.Where(a => a.CreatedAt >=  Top3Accounts).CountAsync();
        }

        public async Task<bool> InactiveAccountAsync(int accountId) => await _accountRepository.InactiveAccountAsync(accountId);

        public async Task<Account?> UpdateAccountAsync(AccountUpdateDto dto)
        {
            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account == null)
            {
                throw new Exception("Conta não encontrada.");
            }
            if (dto.AccountNumber != null)
            {
                account.AccountNumber = dto.AccountNumber;
            }

            if (dto.Balance.HasValue)
            {
                account.Balance = dto.Balance.Value;gi
            }

            if (dto.IsActive.HasValue)
            {
                account.IsActive = dto.IsActive.Value;
            }

            await _accountRepository.UpdateAccountAsync(account);

            return account;
        }
    }
}
