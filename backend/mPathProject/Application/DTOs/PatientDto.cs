namespace mPathProject.Application.DTOs
{
    public class PatientDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Observations { get; set; }
    }
}
