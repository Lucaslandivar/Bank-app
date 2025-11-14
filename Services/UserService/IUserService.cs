using _7Bank.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _7Bank.Api.Services.UserService
{
    public interface IUserService
    {
        Task<Users> CreateUserAsync(Users user, string createdByRole);
        Task<Users?> ValidateLoginAsync(string email, string password);
        Task<Users?> GetByIdAsync(int id);
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByCpfAsync(string cpf);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int id);
    }
}
