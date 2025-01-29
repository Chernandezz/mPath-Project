using System.ComponentModel.DataAnnotations;

namespace mPathProject.Domain.Entities
{
    public class User
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        public string userRole { get; set; }
    }
}
