using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mPathProject.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string UserRole { get; set; } // "Admin", "Doctor", "Patient"

        // Relations
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
