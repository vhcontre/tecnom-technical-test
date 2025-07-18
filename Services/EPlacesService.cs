using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using technical_tests_backend_ssr.Models;

public class EPlacesService : IEPlacesService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<EPlacesService> _logger;
    private readonly IConfiguration _configuration;
    private const string CacheKey = "ActivePlaces";
    private const int CacheMinutes = 5;

    private readonly string _apiUrl;
    private readonly string _apiUser;
    private readonly string _apiPassword;

    public EPlacesService(HttpClient httpClient, IMemoryCache cache, ILogger<EPlacesService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
        _configuration = configuration;

        _apiUrl = _configuration["EPlacesApi:Url"] ?? throw new InvalidOperationException("EPlacesApi:Url is not set in configuration.");
        _apiUser = _configuration["EPlacesApi:User"] ?? throw new InvalidOperationException("EPlacesApi:User is not set in configuration.");
        _apiPassword = _configuration["EPlacesApi:Password"] ?? throw new InvalidOperationException("EPlacesApi:Password is not set in configuration.");
    }

    public async Task<IReadOnlyList<EPlace>> GetActivePlacesAsync()
    {
        if (_cache.TryGetValue(CacheKey, out IReadOnlyList<EPlace> cachedPlaces))
        {
            _logger.LogInformation("Lugares activos obtenidos del caché.");
            return cachedPlaces;
        }

        _logger.LogInformation("Consultando lugares activos desde la API externa.");

        var byteArray = System.Text.Encoding.ASCII.GetBytes($"{_apiUser}:{_apiPassword}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        try
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error al consultar la API externa. StatusCode: {StatusCode}, Reason: {ReasonPhrase}, Content: {Content}",
                    response.StatusCode, response.ReasonPhrase, errorContent);
                throw new HttpRequestException($"Error consultando la API externa. StatusCode: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {errorContent}");
            }
            var json = await response.Content.ReadAsStringAsync();
            var allPlaces = JsonSerializer.Deserialize<List<EPlace>>(json) ?? new List<EPlace>();
            var activePlaces = allPlaces.FindAll(p => p.IsActive);
            _cache.Set(CacheKey, activePlaces, TimeSpan.FromMinutes(CacheMinutes));
            return activePlaces;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción al consultar la API externa: {Message}", ex.Message);
            throw;
        }
    }
}
