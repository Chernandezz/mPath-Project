using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mPathProject.Context;
using mPathProject.Models;

namespace mPathProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            try
            {
                var query = _context.Doctors.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(d => d.firstName.Contains(searchText));
                }

                var totalItems = await query.CountAsync();
                var doctors = await query
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();

                return Ok(new
                {
                    data = doctors,
                    totalItems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var doctor = await _context.Doctors.FindAsync(id);
                if (doctor == null)
                {
                    return NotFound(new { message = "Doctor not found" });
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = doctor.id }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Doctor doctor)
        {
            if (id != doctor.id)
            {
                return BadRequest(new { message = "Mismatched Doctor ID" });
            }

            try
            {
                _context.Entry(doctor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Doctors.Any(d => d.id == id))
                {
                    return NotFound(new { message = "Doctor not found" });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<long> ids)
        {
            try
            {
                var doctors = await _context.Doctors
                    .Where(d => ids.Contains(d.id))
                    .ToListAsync();

                if (!doctors.Any())
                {
                    return NotFound(new { message = "No doctors found to delete" });
                }

                _context.Doctors.RemoveRange(doctors);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
