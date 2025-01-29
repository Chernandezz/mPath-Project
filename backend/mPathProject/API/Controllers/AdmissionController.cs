using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _admissionService;

        public AdmissionController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {
            var admissions = await _admissionService.GetAllAsync(count, page, searchText);
            return Ok(admissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var admission = await _admissionService.GetByIdAsync(id);
            return admission != null ? Ok(admission) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdmissionRequestDto admissionDto)
        {
            var newAdmission = await _admissionService.CreateAsync(admissionDto);
            return CreatedAtAction(nameof(GetById), new { id = newAdmission.Id }, newAdmission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateAdmissionRequestDto admissionDto)
        {
            var updated = await _admissionService.UpdateAsync(id, admissionDto);
            return updated ? NoContent() : NotFound();
        }
    }
}
