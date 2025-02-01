using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mPathProject.Domain.Entities;

namespace mPathProject.Domain.Entities
{
    public class Discharge
    {
        public long Id { get; set; }

        [Required]
        public string Recommendation { get; set; }

        [Required]
        public System.DateTime DischargeDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99)]
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; } = false;
        public bool IsCompleted { get; set; } = false;

        [Required]
        [ForeignKey("Admission")]
        public long AdmissionId { get; set; }

        //Relation with Admission
        public virtual Admission Admission { get; set; }
    }
}

