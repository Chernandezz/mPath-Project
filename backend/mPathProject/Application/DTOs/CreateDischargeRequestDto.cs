namespace mPathProject.Application.DTOs
{
    public class CreateDischargeRequestDto
    {
        public string Recommendation { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Amount { get; set; }
        public long AdmissionId { get; set; }
    }
}
