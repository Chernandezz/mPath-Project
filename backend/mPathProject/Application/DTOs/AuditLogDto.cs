using System;

namespace mPathProject.Application.DTOs
{
    public class AuditLogDto
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public long? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}
