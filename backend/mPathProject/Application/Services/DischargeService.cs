using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using mPathProject.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class DischargeService : IDischargeService
    {
        private readonly IDischargeRepository _dischargeRepository;

        public DischargeService(IDischargeRepository dischargeRepository)
        {
            _dischargeRepository = dischargeRepository;
        }

        public async Task<(List<DischargeDto>, int totalItems)> GetAllRecommendationsByUserIdAsync(int count, int page, string searchText, long userId)
        {
            var (discharges, totalItems) = await _dischargeRepository.GetAllRecommendationsByUserIdAsync(count, page, searchText, userId);
            var dischargeDtos = discharges.Select(d => new DischargeDto
            {
                Id = d.Id,
                Recommendation = d.Recommendation,
                DischargeDate = d.DischargeDate,
                Amount = d.Amount,
                IsPaid = d.IsPaid,
                IsCompleted = d.IsCompleted,
                AdmissionId = d.AdmissionId
            }).ToList();

            return (dischargeDtos, totalItems);
        }
        public async Task<(List<DischargeDto>, int totalItems)> GetAllAsync(int count, int page, string searchText)
        {
            var (discharges, totalItems) = await _dischargeRepository.GetAllAsync(count, page, searchText);
            var dischargeDtos = discharges.Select(d => new DischargeDto
            {
                Id = d.Id,
                Recommendation = d.Recommendation,
                DischargeDate = d.DischargeDate,
                Amount = d.Amount,
                IsPaid = d.IsPaid,
                IsCompleted = d.IsCompleted,
                AdmissionId = d.AdmissionId
            }).ToList();

            return (dischargeDtos, totalItems);
        }
        public async Task<DischargeDto> GetByIdAsync(long id)
        {
            var discharge = await _dischargeRepository.GetByIdAsync(id);
            return discharge == null ? null : new DischargeDto
            {
                Id = discharge.Id,
                Recommendation = discharge.Recommendation,
                DischargeDate = discharge.DischargeDate,
                Amount = discharge.Amount,
                IsPaid = discharge.IsPaid,
                AdmissionId = discharge.AdmissionId
            };
        }

        public async Task<DischargeDto> CreateAsync(CreateDischargeRequestDto dischargeDto)
        {
            var discharge = new Discharge
            {
                Recommendation = dischargeDto.Recommendation,
                DischargeDate = dischargeDto.DischargeDate,
                Amount = dischargeDto.Amount,
                AdmissionId = dischargeDto.AdmissionId
            };

            await _dischargeRepository.AddAsync(discharge);

            return new DischargeDto
            {
                Id = discharge.Id,
                Recommendation = discharge.Recommendation,
                DischargeDate = discharge.DischargeDate,
                Amount = discharge.Amount,
                IsPaid = discharge.IsPaid,
                AdmissionId = discharge.AdmissionId
            };
        }

        public async Task<bool> UpdateAsync(long id, UpdateDischargeRequestDto dischargeDto)
        {
            var discharge = await _dischargeRepository.GetByIdAsync(id);
            if (discharge == null) return false;

            discharge.Recommendation = dischargeDto.Recommendation;
            discharge.DischargeDate = dischargeDto.DischargeDate;
            discharge.Amount = dischargeDto.Amount;
            discharge.AdmissionId = dischargeDto.AdmissionId;

            await _dischargeRepository.UpdateAsync(discharge);
            return true;
        }

        public async Task<bool> DeactivateAsync(long id)
        {
            return await _dischargeRepository.DeactivateAsync(id);
        }

        public async Task<bool> ActivateAsync(long id)
        {
            return await _dischargeRepository.ActivateAsync(id);
        }
    }
}
