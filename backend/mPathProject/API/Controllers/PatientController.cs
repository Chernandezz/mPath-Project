using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            var patients = await _patientService.GetAllAsync(count, page, searchText);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return patient != null ? Ok(patient) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientRequestDto patientDto)
        {
            var newPatient = await _patientService.CreateAsync(patientDto);
            return CreatedAtAction(nameof(GetById), new { id = newPatient.Id }, newPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdatePatientRequestDto patientDto)
        {
            var updated = await _patientService.UpdateAsync(id, patientDto);
            return updated ? NoContent() : NotFound();
        }
    }
}
