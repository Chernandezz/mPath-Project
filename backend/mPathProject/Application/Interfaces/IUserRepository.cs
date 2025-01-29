using mPathProject.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace mPathProject.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(long id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(long id);
        Task<User?> AuthenticateAsync(string email, string password);
    }
}
