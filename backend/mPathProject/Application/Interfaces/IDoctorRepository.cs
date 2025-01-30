using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDoctorRepository
    {
        Task<(List<Doctor> doctors, int totalItems)> GetAllAsync(int count, int page, string searchText);
        Task<Doctor> GetByIdAsync(long id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task<bool> DeactivateAsync(long id);
        Task<bool> ActivateAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}
