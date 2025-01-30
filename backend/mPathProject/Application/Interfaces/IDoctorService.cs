using mPathProject.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<(List<DoctorDto>, int totalItems)> GetAllAsync(int count, int page, string searchText);
        Task<DoctorDto> GetByIdAsync(long id);
        Task<DoctorDto> CreateAsync(CreateDoctorRequestDto doctorDto);
        Task<bool> UpdateAsync(long id, UpdateDoctorRequestDto doctorDto);
        Task<bool> DeactivateAsync(long id);
    }
}
