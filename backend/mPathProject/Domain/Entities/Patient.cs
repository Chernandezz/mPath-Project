using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Patient
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
    [StringLength(500)]
    public string Address { get; set; }

    [Required]
    [StringLength(10)]
    public string PhoneNumber { get; set; }

    public string? Observations { get; set; }

    // Relación with User
    public virtual User User { get; set; }
}
