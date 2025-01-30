using mPathProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
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

    // Relation 1 on 1 with Doctor y Patient
    public virtual Doctor? Doctor { get; set; }
    public virtual Patient? Patient { get; set; }
}
