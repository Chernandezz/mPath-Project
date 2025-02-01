namespace mPathProject.Application.DTOs
{
    public class DischargeDto
    {
        public long Id { get; set; }
        public string Recommendation { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCompleted { get; set; }
        public long AdmissionId { get; set; }
    }
}
