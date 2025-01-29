using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mPathProject.Domain.Entities
{
    public class Doctor
    {
        [Key, ForeignKey("User")]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public bool Active { get; set; }

        // Relación with User
        public virtual User User { get; set; }
    }
}