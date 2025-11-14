using _7Bank.Api.DTOs.Transaction;
using _7Bank.Api.Models;
using _7Bank.Api.Repositories.AccountRepository;
using _7Bank.Api.Repositories.TransactionRepository;
using _7Bank.Api.Repositories.UsersRepository;

namespace _7Bank.Api.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _UserRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _UserRepository = userRepository;
        }

        public async Task<TransactionResponseDto> TransferAsync(
            int fromUserId,
            string identifier,
            string identifierType,
            decimal amount)
        {
            var response = new TransactionResponseDto();

            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Valor deve ser maior que zero.";
                return response;
            }
            if (string.IsNullOrEmpty(identifier))
            {
                response.Success = false;
                response.Message = "Informar Email ou CPF.";
                return response;
            }

            var fromAccount = await _accountRepository.GetByUserIdAsync(fromUserId);
            if (fromAccount == null)
            {
                response.Success = false;
                response.Message = "Conta de origem não encontrada.";
                return response;
            }

            var targetUserEmail = await _UserRepository.GetByEmailAsync(identifier);
            var targetUserCpf = await _UserRepository.GetByCpfAsync(identifier);

            var targetUser = targetUserEmail ?? targetUserCpf;

            if (targetUser == null)
            {
                response.Success = false;
                response.Message = "Usuário destino não encontrado.";
                return response;
            }

            var targetAccount = await _accountRepository.GetByUserIdAsync(targetUser.UserId);

            if (fromAccount.Balance < amount)
            {
                response.Success = false;
                response.Message = "Saldo Insuficente.";
                return response;
            }
            fromAccount.Balance -= amount;
            targetAccount.Balance += amount;

            await _accountRepository.UpdateAccountAsync(fromAccount);
            await _accountRepository.UpdateAccountAsync(targetAccount);

            var debitTransaction = new Transaction
            {
                AccountId = fromAccount.AccountId,
                Amount = amount,
                Type = TransactionType.DEBIT,
                Date = DateTime.Now
            };
            await _transactionRepository.CreateAsync(debitTransaction);

            var creditTransaction = new Transaction
            {
                AccountId = targetAccount.AccountId,
                Amount = amount,
                Type = TransactionType.CREDIT,
                Date = DateTime.Now
            };
            await _transactionRepository.CreateAsync(creditTransaction);

            response.Success = true;
            response.Message = "Transferência realizada com sucesso!";

            return response;
        }
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId)
        {
            var account = await _accountRepository.GetByUserIdAsync(userId);

            if(account == null)
            {
                return Enumerable.Empty<Transaction>();
            }
            return await _transactionRepository.GetByAccountIdAsync(account.AccountId);
        }

        public async Task<IEnumerable<Transaction>> GetLast3MonthsAsync(int userId)
        {
            var account = await _accountRepository.GetByUserIdAsync(userId);
            if(account == null)
            {
                return Enumerable.Empty<Transaction>();
            }
            return await _transactionRepository.GetLastMonthsAsync(account.AccountId, 3);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }
    }
}
