using System.ComponentModel.DataAnnotations;

namespace mPathProject.Models
{
    public class Doctor
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        public string lastName { get; set; }
        [Required]
        public bool active { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string email { get; set; }
    }
}
