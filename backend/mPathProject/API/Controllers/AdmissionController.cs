using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mPathProject.Domain.Entities;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdmissionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            try
            {
                var query = _context.Admissions.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(a => a.observation.Contains(searchText));
                }

                var totalItems = await query.CountAsync();
                var admissions = await query
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();

                return Ok(new { data = admissions, totalItems });
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
                var admission = await _context.Admissions.FindAsync(id);
                if (admission == null)
                {
                    return NotFound(new { message = "Admission not found" });
                }
                return Ok(admission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Admission admission)
        {
            try
            {
                _context.Admissions.Add(admission);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { admission.id }, admission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Admission admission)
        {
            if (id != admission.id)
            {
                return BadRequest(new { message = "Mismatched Admission ID" });
            }

            try
            {
                _context.Entry(admission).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Admissions.Any(a => a.id == id))
                {
                    return NotFound(new { message = "Admission not found" });
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
