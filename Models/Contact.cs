using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }
}
