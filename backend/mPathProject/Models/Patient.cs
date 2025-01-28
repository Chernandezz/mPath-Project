using System.ComponentModel.DataAnnotations;

namespace mPathProject.Models
{
    public class Patient
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        public string lastName { get; set; }
        [Required]
        [StringLength(500)]
        public string address { get; set; }
        [Required]
        [StringLength(10)]
        public string phoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string email { get; set; }
        public string observations { get; set; }
    }
}
