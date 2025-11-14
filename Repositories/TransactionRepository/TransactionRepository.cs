using _7Bank.Api.Data;
using _7Bank.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace _7Bank.Api.Repositories.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankDbContext _context;

        public TransactionRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
        {
            return await _context.Transactions.Where(t => t.AccountId == accountId).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetLastMonthsAsync(int accountId, int months)
        {
            var dataLimit = DateTime.Now.AddMonths(-months);

            return await _context.Transactions.Where(t => t.AccountId == accountId && t.Date >= dataLimit).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<bool> HasTransactionsAsync(int accountId)
        {
            return await _context.Transactions.AnyAsync(t  => t.AccountId == accountId);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

    }
}
