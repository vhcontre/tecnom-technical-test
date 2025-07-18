using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace technical_tests_backend_ssr.Models;

public class EPlace
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("active")]
    public bool IsActive { get; set; }
}
