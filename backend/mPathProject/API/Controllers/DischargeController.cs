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
    public class DischargeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DischargeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            try
            {
                var query = _context.Discharges.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(d => d.treatment.Contains(searchText));
                }

                var totalItems = await query.CountAsync();
                var discharges = await query
                    .Skip(page * count)
                    .Take(count)
                    .ToListAsync();

                return Ok(new { data = discharges, totalItems });
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
                var discharge = await _context.Discharges.FindAsync(id);
                if (discharge == null)
                {
                    return NotFound(new { message = "Discharge not found" });
                }
                return Ok(discharge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Discharge discharge)
        {
            try
            {
                _context.Discharges.Add(discharge);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { discharge.id }, discharge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Discharge discharge)
        {
            if (id != discharge.id)
            {
                return BadRequest(new { message = "Mismatched Discharge ID" });
            }

            try
            {
                _context.Entry(discharge).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Discharges.Any(d => d.id == id))
                {
                    return NotFound(new { message = "Discharge not found" });
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
