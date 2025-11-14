using _7Bank.Api.Data;
using _7Bank.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace _7Bank.Api.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            if (await GetByIdAsync(accountId) == null) return false;
            if (await HaveTransactionsAsync(accountId)) throw new Exception("Não é possível excluir conta que possue movimentações.");

            Account? account = await GetByIdAsync(accountId);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<Account?> GetByUserIdAsync(int userId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<bool> HaveTransactionsAsync(int accountId)
        {
            return await _context.Transactions.AnyAsync(t => t.AccountId == accountId);
        }

        public async Task<bool> InactiveAccountAsync(int accountId)
        {
            var account = await GetByIdAsync(accountId);
            if (account == null) return false;

            account.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

    }
}
