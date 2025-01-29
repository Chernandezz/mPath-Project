using mPathProject.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(long id);
        Task<UserDto> CreateAsync(CreateUserRequestDto userDto);
        Task<bool> UpdateAsync(long id, UpdateUserRequestDto userDto);
        Task<bool> DeleteAsync(long id);
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto loginDto);
    }
}
