using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class PlacesService : IPlacesService
{
    private readonly IEPlacesService _ePlacesService;
    private readonly ILogger<PlacesService> _logger;

    public PlacesService(IEPlacesService ePlacesService, ILogger<PlacesService> logger)
    {
        _ePlacesService = ePlacesService;
        _logger = logger;
    }

    public async Task<bool> IsPlaceActiveAsync(int placeId)
    {
        var activePlaces = await _ePlacesService.GetActivePlacesAsync();
        _logger.LogInformation("Cantidad de talleres activos recibidos: {Count}", activePlaces.Count);
        foreach (var p in activePlaces)
        {
            _logger.LogInformation("Taller activo: Id={Id}, Name={Name}, IsActive={IsActive}", p.Id, p.Name, p.IsActive);
        }
        bool exists = activePlaces.Any(p => p.Id == placeId && p.IsActive);
        _logger.LogInformation("Validación de lugar {PlaceId}: {Exists}", placeId, exists);
        return exists;
    }
}
