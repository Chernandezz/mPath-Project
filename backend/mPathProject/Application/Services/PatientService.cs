using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using mPathProject.Infrastructure.Authentication;
using mPathProject.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class PatientService : IPatientService
    {

        private readonly IPatientRepository _patientRepository;
        private readonly IUserService _userService;

        public PatientService(IPatientRepository patientRepository, IUserService userService)
        {
            _patientRepository = patientRepository;
            _userService = userService;
        }

        public async Task<(List<PatientDto>, int totalItems)> GetAllAsync(int count, int page, string searchText)
        {
            var (patients, totalItems) = await _patientRepository.GetAllAsync(count, page, searchText);

            var patientsDtos = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
                Observations = p.Observations
            }).ToList();

            return (patientsDtos, totalItems);
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
            var user = await _userService.CreateAsync(new CreateUserRequestDto
            {
                Email = patientDto.Email,
                Password = PasswordHashHandler.HashPassword(patientDto.Password),
                UserRole = "Patient"
            });


            var patient = new Patient
            {
                Id = user.Id,
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
