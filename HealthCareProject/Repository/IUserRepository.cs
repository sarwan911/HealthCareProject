using HealthCareProject.Models;
using HealthCareProject.Repository;

namespace HealthCareProject.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<User> GetByEmailAsync(string email);
    }
}