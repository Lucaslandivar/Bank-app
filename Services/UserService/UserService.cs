using _7Bank.Api.Data;
using _7Bank.Api.Models;
using _7Bank.Api.Repositories.UsersRepository;
using _7Bank.Api.Services.UserService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace _7Bank.Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly BankDbContext _context;

        public UserService(IUserRepository repo, BankDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Users> CreateUserAsync(Users u, string role)
        {
            if (u.Password.Length < 8) throw new Exception("Devem ser 8 caracteres no mínimo");

            if (await _repo.GetByEmailAsync(u.Email) != null) throw new Exception("Email já existe");

            if (await _repo.GetByCpfAsync(u.Cpf) != null) throw new Exception("CPF vinculado a conta existente");

            u.Password = Hash(u.Password);
            var saved = await _repo.CreateUserAsync(u);

            var acc = new Account
            {
                UserId = saved.UserId,
                AccountNumber = new Random().Next(100000, 999999).ToString(),
                Balance = 0
            };
            _context.Accounts.Add(acc);
            await _context.SaveChangesAsync();

            return saved;
        }

        private string Hash(string s)
        {
            var sha = SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(s)));
        }

        public Task<Users?> ValidateLoginAsync(string e, string p) => _repo.ValidateLoginAsync(e, Hash(p));

        public Task<Users?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Users?> GetByEmailAsync(string e) => _repo.GetByEmailAsync(e);
        public Task<Users?> GetByCpfAsync(string c) => _repo.GetByCpfAsync(c);
        public Task<IEnumerable<Users>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Users> UpdateUserAsync(Users u) => _repo.UpdateUserAsync(u);
        public Task<bool> DeleteUserAsync(int id) => _repo.DeleteUserAsync(id);

    }
}
