using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mPathProject.Context;
using mPathProject.Models;
using mPathProject.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using mPathProject.Services;

namespace mPathProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;

        public UserController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result is null)
                return Unauthorized();

            return result;
        }



        // POST: api/User (Crear usuario con contraseña hasheada)
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.email) ||
                string.IsNullOrWhiteSpace(user.password) ||
                string.IsNullOrWhiteSpace(user.userRole))
            {
                return BadRequest("Invalid Request");
            }

            // Hashear la contraseña antes de guardarla
            user.password = PasswordHashHandler.HashPassword(user.password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
        }

        // PUT: api/User/{id} (Actualizar usuario con verificación de contraseña)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Si se proporciona una nueva contraseña, hashearla
            if (!string.IsNullOrWhiteSpace(user.password))
            {
                existingUser.password = PasswordHashHandler.HashPassword(user.password);
            }

            existingUser.email = user.email;
            existingUser.userRole = user.userRole;

            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
