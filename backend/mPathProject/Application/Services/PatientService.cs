using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> GetAllAsync(int count, int page, string searchText)
        {
            var patients = await _patientRepository.GetAllAsync(count, page, searchText);
            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Observations = p.Observations
            }).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(long id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return patient == null ? null : new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Observations = patient.Observations
            };
        }

        public async Task<PatientDto> CreateAsync(CreatePatientRequestDto patientDto)
        {
            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                Address = patientDto.Address,
                PhoneNumber = patientDto.PhoneNumber,
                Observations = patientDto.Observations
            };

            await _patientRepository.AddAsync(patient);

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Observations = patient.Observations
            };
        }

        public async Task<bool> UpdateAsync(long id, UpdatePatientRequestDto patientDto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return false;

            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.Address = patientDto.Address;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.Observations = patientDto.Observations;

            await _patientRepository.UpdateAsync(patient);
            return true;
        }

        public async Task<bool> DeleteAsync(List<long> ids)
        {
            await _patientRepository.DeleteAsync(ids);
            return true;
        }
    }
}
