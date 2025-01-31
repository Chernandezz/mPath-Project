using mPathProject.Application.DTOs;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<(List<Patient>, int totaItems)> GetAllAsync(int count, int page, string searchText);
        Task<Patient> GetByIdAsync(long id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(List<long> ids);
        Task<bool> ExistsAsync(long id);
    }
}
