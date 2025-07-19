using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Data;
using Microsoft.EntityFrameworkCore;

public class LeadService : ILeadService
{
    private readonly AppDbContext _dbContext;
    private readonly IPlacesService _placesService;
    private readonly ILogger<LeadService> _logger;

    public LeadService(AppDbContext dbContext, IPlacesService placesService, ILogger<LeadService> logger)
    {
        _dbContext = dbContext;
        _placesService = placesService;
        _logger = logger;
    }

    public async Task<Lead> CreateLeadAsync(Lead lead)
    {
        _logger.LogInformation("Validando Lead para el lugar {PlaceId}", lead.PlaceId);

        // Validar que el place_id esté en los talleres activos usando PlacesService
        bool isActive = await _placesService.IsPlaceActiveAsync(lead.PlaceId);

        if (!isActive)
        {
            _logger.LogWarning("El lugar {PlaceId} no está activo o no existe", lead.PlaceId);
            throw new InvalidOperationException("El lugar especificado no está activo o no existe.");
        }

        _dbContext.Leads.Add(lead);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Lead creado exitosamente con ID {Id}", lead.Id);
        return lead;
    }

    public async Task<Lead?> GetLeadByIdAsync(int id)
    {
        _logger.LogInformation("Buscando Lead con ID {Id}", id);
        return await _dbContext.Leads
            .Include(l => l.Contact)
            .Include(l => l.Vehicle)
            .FirstOrDefaultAsync(l => l.Id == id);
    }
}
