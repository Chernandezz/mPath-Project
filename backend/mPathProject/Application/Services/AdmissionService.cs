using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly IAdmissionRepository _admissionRepository;

        public AdmissionService(IAdmissionRepository admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        public async Task<(List<AdmissionDto>, int totalItems)> GetAllAsync(int count, int page, string searchText)
        {
            var (admissions, totalItems) = await _admissionRepository.GetAllAsync(count, page, searchText);
            var admissionDtos = admissions.Select(a => new AdmissionDto
            {
                Id = a.Id,
                AdmissionDate = a.AdmissionDate,
                Diagnosis = a.Diagnosis,
                Observation = a.Observation,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId
            }).ToList();

            return (admissionDtos, totalItems);
        }

        public async Task<AdmissionDto> GetByIdAsync(long id)
        {
            var admission = await _admissionRepository.GetByIdAsync(id);
            return admission == null ? null : new AdmissionDto
            {
                Id = admission.Id,
                AdmissionDate = admission.AdmissionDate,
                Diagnosis = admission.Diagnosis,
                Observation = admission.Observation,
                DoctorId = admission.DoctorId,
                PatientId = admission.PatientId
            };
        }

        public async Task<AdmissionDto> CreateAsync(CreateAdmissionRequestDto admissionDto)
        {
            var admission = new Admission
            {
                AdmissionDate = admissionDto.AdmissionDate,
                Diagnosis = admissionDto.Diagnosis,
                Observation = admissionDto.Observation,
                DoctorId = admissionDto.DoctorId,
                PatientId = admissionDto.PatientId
            };

            await _admissionRepository.AddAsync(admission);

            return new AdmissionDto
            {
                Id = admission.Id,
                AdmissionDate = admission.AdmissionDate,
                Diagnosis = admission.Diagnosis,
                Observation = admission.Observation,
                DoctorId = admission.DoctorId,
                PatientId = admission.PatientId
            };
        }

        public async Task<bool> UpdateAsync(long id, UpdateAdmissionRequestDto admissionDto)
        {
            var admission = await _admissionRepository.GetByIdAsync(id);
            if (admission == null) return false;

            admission.AdmissionDate = admissionDto.AdmissionDate;
            admission.Diagnosis = admissionDto.Diagnosis;
            admission.Observation = admissionDto.Observation;
            admission.DoctorId = admissionDto.DoctorId;
            admission.PatientId = admissionDto.PatientId;

            await _admissionRepository.UpdateAsync(admission);
            return true;
        }
    }
}
