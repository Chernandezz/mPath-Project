using mPathProject.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IAdmissionService
    {
        Task<(List<AdmissionDto>, int totalItems)> GetAllAsync(int count, int page, string searchText);
        Task<AdmissionDto> GetByIdAsync(long id);
        Task<AdmissionDto> CreateAsync(CreateAdmissionRequestDto admissionDto);
        Task<bool> UpdateAsync(long id, UpdateAdmissionRequestDto admissionDto);
    }
}
