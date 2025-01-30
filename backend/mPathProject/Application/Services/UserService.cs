using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using mPathProject.Infrastructure.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                UserRole = u.UserRole
            }).ToList();
        }

        public async Task<UserDto> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }

        public async Task<UserDto> CreateAsync(CreateUserRequestDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Password = PasswordHashHandler.HashPassword(userDto.Password),
                UserRole = userDto.UserRole
            };

            await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }

        public async Task<bool> UpdateAsync(long id, UpdateUserRequestDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                user.Password = PasswordHashHandler.HashPassword(userDto.Password);
            }

            user.Email = userDto.Email;
            user.UserRole = userDto.UserRole;

            await _userRepository.UpdateAsync(user);
            return true;
        }


        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto loginDto)
        {
            return await _jwtService.Authenticate(loginDto);
        }
    }
}
