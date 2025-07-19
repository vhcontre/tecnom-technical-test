using System.ComponentModel.DataAnnotations;

namespace technical_tests_backend_ssr.Models;

public class Lead
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PlaceId { get; set; }

    [Required]
    public DateTime AppointmentAt { get; set; }

    [Required]
    [EnumDataType(typeof(ServiceType))]
    public ServiceType ServiceType { get; set; }

    [Required]
    public Contact Contact { get; set; } = new Contact();

    public Vehicle? Vehicle { get; set; }
}
