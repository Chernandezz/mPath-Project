using mPathProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using mPathProject.Application.DTOs;

namespace mPathProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int count = 10, int page = 0)
        {
            List<AuditLogDto> logs = await _auditLogService.GetAllAsync(count, page);
            return Ok(new { data = logs });
        }

        [HttpPost("create-log")]
        public async Task<IActionResult> CreateLog([FromBody] CreateAuditLogDto logDto)
        {
            await _auditLogService.LogActionAsync(logDto.UserId, logDto.Action, logDto.Entity, logDto.EntityId, logDto.Details);
            return Ok(new { message = "Log created successfully" });
        }
    }
}
