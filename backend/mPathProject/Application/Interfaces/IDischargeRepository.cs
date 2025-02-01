using mPathProject.Application.DTOs;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDischargeRepository
    {
        Task<(List<Discharge>, int totaItems)> GetAllAsync(int count, int page, string searchText);
        Task<(List<Discharge>, int totalItems)> GetAllRecommendationsByUserIdAsync(int count, int page, string searchText, long userId);
        Task<Discharge> GetByIdAsync(long id);
        Task AddAsync(Discharge discharge);
        Task<bool> DeactivateAsync(long id);
        Task<bool> ActivateAsync(long id);
        Task UpdateAsync(Discharge discharge);
        Task<bool> ExistsAsync(long id);
    }
}
