using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorDto>> GetAllAsync(int count, int page, string searchText)
        {
            var doctors = await _doctorRepository.GetAllAsync(count, page, searchText);
            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Active = d.Active
            }).ToList();
        }

        public async Task<DoctorDto> GetByIdAsync(long id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            return doctor == null ? null : new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Active = doctor.Active
            };
        }

        public async Task<DoctorDto> CreateAsync(CreateDoctorRequestDto doctorDto)
        {
            var doctor = new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Active = doctorDto.Active
            };

            await _doctorRepository.AddAsync(doctor);

            return new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Active = doctor.Active
            };
        }

        public async Task<bool> UpdateAsync(long id, UpdateDoctorRequestDto doctorDto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return false;

            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Active = doctorDto.Active;

            await _doctorRepository.UpdateAsync(doctor);
            return true;
        }

        public async Task<bool> DeleteAsync(List<long> ids)
        {
            await _doctorRepository.DeleteAsync(ids);
            return true;
        }
    }
}
