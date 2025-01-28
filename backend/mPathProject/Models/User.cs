using System.ComponentModel.DataAnnotations;

namespace mPathProject.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }
    }
}
