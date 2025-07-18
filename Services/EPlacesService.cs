using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
    private const string ApiUrl = "https://dev.tecnomcrm.com/api/v1/places/workshops";
    private const string ApiUser = "max@tecnom.com.ar";
    private const string ApiPassword = "b0x3sApp";

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

        // Autenticación básica
        var byteArray = System.Text.Encoding.ASCII.GetBytes($"{ApiUser}:{ApiPassword}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        var response = await _httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var allPlaces = JsonSerializer.Deserialize<List<EPlace>>(json) ?? new List<EPlace>();
        var activePlaces = allPlaces.FindAll(p => p.IsActive);
        _cache.Set(CacheKey, activePlaces, TimeSpan.FromMinutes(CacheMinutes));
        return activePlaces;
    }
}
