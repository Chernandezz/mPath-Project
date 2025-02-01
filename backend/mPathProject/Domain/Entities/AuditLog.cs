using System;
using System.ComponentModel.DataAnnotations;

namespace mPathProject.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Entity { get; set; }

        public long? EntityId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Details { get; set; }
    }
}
