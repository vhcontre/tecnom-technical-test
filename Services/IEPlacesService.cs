using System.Collections.Generic;
using System.Threading.Tasks;
using technical_tests_backend_ssr.Models;

public interface IEPlacesService
{
    Task<IReadOnlyList<EPlace>> GetActivePlacesAsync();
}
