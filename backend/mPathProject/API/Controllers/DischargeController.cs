using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mPathProject.Application.Services;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DischargeController : ControllerBase
    {
        private readonly IDischargeService _dischargeService;

        public DischargeController(IDischargeService dischargeService)
        {
            _dischargeService = dischargeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int count = 10, int page = 0, string searchText = null)
        {

            (List<DischargeDto> discharges, int totalItems) = await _dischargeService.GetAllAsync(count, page, searchText);

            var response = new
            {
                data = discharges,
                totalItems
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var discharge = await _dischargeService.GetByIdAsync(id);
            return discharge != null ? Ok(discharge) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDischargeRequestDto dischargeDto)
        {
            var newDischarge = await _dischargeService.CreateAsync(dischargeDto);
            return CreatedAtAction(nameof(GetById), new { id = newDischarge.Id }, newDischarge);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateDischargeRequestDto dischargeDto)
        {
            var updated = await _dischargeService.UpdateAsync(id, dischargeDto);
            return updated ? NoContent() : NotFound();
        }
    }
}
