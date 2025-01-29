using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mPathProject.Domain.Entities
{
    public class Admission
    {
        public long Id { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        public string? Observation { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [Required]
        public long PatientId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
