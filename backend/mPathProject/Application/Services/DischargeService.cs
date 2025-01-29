using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
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

        public async Task<List<DischargeDto>> GetAllAsync(int count, int page, string searchText)
        {
            var discharges = await _dischargeRepository.GetAllAsync(count, page, searchText);
            return discharges.Select(d => new DischargeDto
            {
                Id = d.Id,
                Treatment = d.Treatment,
                DischargeDate = d.DischargeDate,
                Amount = d.Amount,
                IsPaid = d.IsPaid,
                AdmissionId = d.AdmissionId
            }).ToList();
        }

        public async Task<DischargeDto> GetByIdAsync(long id)
        {
            var discharge = await _dischargeRepository.GetByIdAsync(id);
            return discharge == null ? null : new DischargeDto
            {
                Id = discharge.Id,
                Treatment = discharge.Treatment,
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
                Treatment = dischargeDto.Treatment,
                DischargeDate = dischargeDto.DischargeDate,
                Amount = dischargeDto.Amount,
                IsPaid = dischargeDto.IsPaid,
                AdmissionId = dischargeDto.AdmissionId
            };

            await _dischargeRepository.AddAsync(discharge);

            return new DischargeDto
            {
                Id = discharge.Id,
                Treatment = discharge.Treatment,
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

            discharge.Treatment = dischargeDto.Treatment;
            discharge.DischargeDate = dischargeDto.DischargeDate;
            discharge.Amount = dischargeDto.Amount;
            discharge.IsPaid = dischargeDto.IsPaid;
            discharge.AdmissionId = dischargeDto.AdmissionId;

            await _dischargeRepository.UpdateAsync(discharge);
            return true;
        }
    }
}
