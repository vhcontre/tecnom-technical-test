using System.Threading.Tasks;

public interface IPlacesService
{
    Task<bool> IsPlaceActiveAsync(int placeId);
}
