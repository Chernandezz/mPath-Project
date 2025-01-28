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
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            try
            {
                var query = _context.Patients.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(p => p.firstName.Contains(searchText));
                }

                var totalItems = await query.CountAsync();
                var patients = await query
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();

                return Ok(new { data = patients, totalItems });
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
                var patient = await _context.Patients.FindAsync(id);
                if (patient == null)
                {
                    return NotFound(new { message = "Patient not found" });
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = patient.id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Patient patient)
        {
            if (id != patient.id)
            {
                return BadRequest(new { message = "Mismatched Patient ID" });
            }

            try
            {
                _context.Entry(patient).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Patients.Any(p => p.id == id))
                {
                    return NotFound(new { message = "Patient not found" });
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
                var patients = await _context.Patients
                    .Where(p => ids.Contains(p.id))
                    .ToListAsync();

                if (!patients.Any())
                {
                    return NotFound(new { message = "No patients found to delete" });
                }

                _context.Patients.RemoveRange(patients);
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
