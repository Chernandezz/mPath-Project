using mPathProject.Application.DTOs;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDischargeService
    {
        Task<(List<DischargeDto>, int totalItems)> GetAllAsync(int count, int page, string searchText);
        Task<(List<DischargeDto>, int totalItems)> GetAllRecommendationsByUserIdAsync(int count, int page, string searchText, long userId);
        Task<DischargeDto> GetByIdAsync(long id);
        Task<DischargeDto> CreateAsync(CreateDischargeRequestDto dischargeDto);
        Task<bool> UpdateAsync(long id, UpdateDischargeRequestDto dischargeDto);
        Task<bool> DeactivateAsync(long id);
        Task<bool> ActivateAsync(long id);
    }
}
