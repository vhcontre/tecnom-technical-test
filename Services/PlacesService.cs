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
        //var activePlaces = await _ePlacesService.GetActivePlacesAsync();
        //bool exists = activePlaces.Any(p => p.Id == placeId);
        //_logger.LogInformation("Validación de lugar {PlaceId}: {Exists}", placeId, exists);
        //return exists;
        return true;
    }
}
