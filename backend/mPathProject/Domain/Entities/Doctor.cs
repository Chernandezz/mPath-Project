using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    [ForeignKey("User")]
    public long Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public bool Active { get; set; }

    // Relation with User
    public virtual User User { get; set; }
}
