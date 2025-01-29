using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDischargeRepository
    {
        Task<List<Discharge>> GetAllAsync(int count, int page, string searchText);
        Task<Discharge> GetByIdAsync(long id);
        Task AddAsync(Discharge discharge);
        Task UpdateAsync(Discharge discharge);
        Task<bool> ExistsAsync(long id);
    }
}
