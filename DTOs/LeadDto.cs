using System.Text.Json.Serialization;

public class ContactDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;
}

public class VehicleDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("make")]
    public string Make { get; set; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("license_plate")]
    public string LicensePlate { get; set; } = string.Empty;
}

public class LeadDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("place_id")]
    public int PlaceId { get; set; }

    [JsonPropertyName("appointment_at")]
    public DateTime AppointmentAt { get; set; }

    [JsonPropertyName("service_type")]
    public string ServiceType { get; set; } = string.Empty;

    [JsonPropertyName("contact")]
    public ContactDto Contact { get; set; } = new ContactDto();

    [JsonPropertyName("vehicle")]
    public VehicleDto? Vehicle { get; set; }
}
