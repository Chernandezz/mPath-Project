using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mPathProject.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            (List<AdmissionDto> admissions, int totalItems) = await _admissionService.GetAllAsync(count, page, searchText);

            var response = new
            {
                data = admissions,
                totalItems
            };

            return Ok(response);
        }

        [HttpGet("admissionbyid")]
        public async Task<IActionResult> GetAllByPatientIdAsync(int count = 10, int page = 0, string searchText = null, long userId = 0)
        {
            (List<AdmissionDto> admissions, int totalItems) = await _admissionService.GetAllByPatientIdAsync(count, page, searchText, userId);

            var response = new
            {
                data = admissions,
                totalItems
            };

            return Ok(response);
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
