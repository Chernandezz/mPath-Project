namespace mPathProject.Application.DTOs
{
    public class CreateAuditLogDto
    {
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public long? EntityId { get; set; }
        public string Details { get; set; }
    }
}