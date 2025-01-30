using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mPathProject.Domain.Entities;
using mPathProject.Infrastructure.Persistence;
using System.Numerics;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int count = 10, int page = 0, string searchText = null)
        {
            (List<DoctorDto> doctors, int totalItems) = await _doctorService.GetAllAsync(count, page, searchText);

            var response = new
            {
                data = doctors,
                totalItems
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return doctor != null ? Ok(doctor) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorRequestDto doctorDto)
        {
            var newDoctor = await _doctorService.CreateAsync(doctorDto);
            return CreatedAtAction(nameof(GetById), new { id = newDoctor.Id }, newDoctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateDoctorRequestDto doctorDto)
        {
            var updated = await _doctorService.UpdateAsync(id, doctorDto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool deleted = await _doctorService.DeactivateAsync(id);

            if (!deleted)
                return NotFound(new { message = "Doctor not found" });

            return NoContent();
        }
    }
}
