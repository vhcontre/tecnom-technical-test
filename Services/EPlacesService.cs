using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using technical_tests_backend_ssr.Models;

public class EPlacesService : IEPlacesService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<EPlacesService> _logger;
    private const string CacheKey = "ActivePlaces";
    private const int CacheMinutes = 5;
    private const string ApiUrl = "https://api.example.com/places"; // Cambia por la URL real

    public EPlacesService(HttpClient httpClient, IMemoryCache cache, ILogger<EPlacesService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IReadOnlyList<EPlace>> GetActivePlacesAsync()
    {
        if (_cache.TryGetValue(CacheKey, out IReadOnlyList<EPlace> cachedPlaces))
        {
            _logger.LogInformation("Lugares activos obtenidos del caché.");
            return cachedPlaces;
        }

        _logger.LogInformation("Consultando lugares activos desde la API externa.");
        var response = await _httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var allPlaces = JsonSerializer.Deserialize<List<EPlace>>(json) ?? new List<EPlace>();
        var activePlaces = allPlaces.FindAll(p => p.IsActive);
        _cache.Set(CacheKey, activePlaces, TimeSpan.FromMinutes(CacheMinutes));
        return activePlaces;
    }
}
