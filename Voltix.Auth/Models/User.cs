using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voltix.Auth.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(100)]
    public string Email { get; set; }
    [Required]
    [StringLength(100)]
    public string PasswordHash { get; set; }

    [Required] [StringLength(50)] public string Role { get; set; } = "contributor";
}