using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mPathProject.Domain.Entities
{
    public class Discharge
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string treatment { get; set; }

        [Required]
        [StringLength(100)]
        public string doctorName { get; set; }

        [Required]
        [StringLength(100)]
        public string patientName { get; set; }

        [Required]
        public DateTime dischargeDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99)]
        public decimal amount { get; set; }

        [Required]
        public long doctorId { get; set; }

        [Required]
        public long admissionId { get; set; }
    }
}
