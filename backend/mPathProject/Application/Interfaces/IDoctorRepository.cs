using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync(int count, int page, string searchText);
        Task<Doctor> GetByIdAsync(long id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(List<long> ids);
        Task<bool> ExistsAsync(long id);
    }
}
