using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

public class EPlace
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
