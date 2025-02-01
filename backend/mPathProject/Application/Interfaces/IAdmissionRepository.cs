using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IAdmissionRepository
    {
        Task<(List<Admission>, int totaItems)> GetAllAsync(int count, int page, string searchText);
        Task<(List<Admission>, int totalItems)> GetAllByPatientIdAsync(int count, int page, string searchText, long patientId);
        Task<Admission> GetByIdAsync(long id);
        Task AddAsync(Admission admission);
        Task UpdateAsync(Admission admission);
        Task<bool> ExistsAsync(long id);
    }
}
