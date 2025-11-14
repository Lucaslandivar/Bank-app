using _7Bank.Api.Models;

namespace _7Bank.Api.Repositories.UsersRepository
{
    public interface IUserRepository
    {
        Task<Users> CreateUserAsync(Users user);
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByCpfAsync(string cpf);
        Task<Users?> GetByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int id);
        Task<Users?> ValidateLoginAsync(string email, string password);
    }
}
