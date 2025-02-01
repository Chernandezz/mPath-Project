using mPathProject.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mPathProject.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<List<AuditLogDto>> GetAllAsync(int count, int page);
        Task LogActionAsync(string userId, string action, string entity, long? entityId, string details = null);
    }
}
