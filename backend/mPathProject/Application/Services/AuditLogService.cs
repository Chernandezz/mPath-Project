using Microsoft.AspNetCore.Http;
using mPathProject.Application.DTOs;
using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<List<AuditLogDto>> GetAllAsync(int count, int page)
        {
            var logs = await _auditLogRepository.GetAllAsync(count, page);
            return logs.Select(log => new AuditLogDto
            {
                Id = log.Id,
                UserId = log.UserId,
                Action = log.Action,
                Entity = log.Entity,
                EntityId = log.EntityId,
                Timestamp = log.Timestamp,
                Details = log.Details
            }).ToList();
        }


        public async Task LogActionAsync(string userId, string action, string entity, long? entityId, string details = null)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Entity = entity,
                EntityId = entityId,
                Details = details
            };

            await _auditLogRepository.AddAsync(log);
        }
    }
}
