namespace mPathProject.Application.DTOs
{
    public class UpdateDischargeRequestDto
    {
        public string Treatment { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public long AdmissionId { get; set; }
    }
}
