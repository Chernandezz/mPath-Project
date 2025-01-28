using System.ComponentModel.DataAnnotations;

namespace mPathProject.Models
{
    public class Discharge
    {
        public long id { get; set; }
        public string treatment { get; set; }
        public string doctorName { get; set; }
        public string patientName { get; set; }

        [Required]
        public System.DateTime dischargeDate { get; set; }
        [Required]
        [Range(0, 9999999.99)]
        public decimal amount { get; set; }
        [Required]
        public long doctorId { get; set; }
        [Required]
        public long admissionId { get; set; }
    }
}
