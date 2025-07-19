using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

public class Vehicle
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Make { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    public string LicensePlate { get; set; } = string.Empty;
}
