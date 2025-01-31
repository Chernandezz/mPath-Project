using mPathProject.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IPatientService
    {
        Task<(List<PatientDto>, int totalItems)> GetAllAsync(int count, int page, string searchText);
        Task<PatientDto> GetByIdAsync(long id);
        Task<PatientDto> CreateAsync(CreatePatientRequestDto patientDto);
        Task<bool> UpdateAsync(long id, UpdatePatientRequestDto patientDto);
        Task<bool> DeleteAsync(List<long> ids);
    }
}
