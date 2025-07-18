using Microsoft.AspNetCore.Mvc;
using technical_tests_backend_ssr.Models;
using FluentValidation;
using AutoMapper;

namespace technical_tests_backend_ssr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadService _leadService;
    private readonly IValidator<LeadDto> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<LeadsController> _logger;

    public LeadsController(
        ILeadService leadService,
        IValidator<LeadDto> validator,
        IMapper mapper,
        ILogger<LeadsController> logger)
    {
        _leadService = leadService;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    // POST /api/leads
    [HttpPost]
    public async Task<IActionResult> CreateLead([FromBody] LeadDto leadDto)
    {
        var validationResult = await _validator.ValidateAsync(leadDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Datos inválidos para Lead: {Errors}", validationResult.Errors);
            return BadRequest(validationResult.ToDictionary());
        }

        var lead = _mapper.Map<Lead>(leadDto);

        try
        {
            var createdLead = await _leadService.CreateLeadAsync(lead);
            return CreatedAtAction(nameof(GetLeadById), new { id = createdLead.PlaceId }, createdLead);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida al crear Lead");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al crear Lead");
            return StatusCode(500, new { message = "Error interno al crear el Lead." });
        }
    }

    // GET /api/leads/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeadById(int id)
    {
        var lead = await _leadService.GetLeadByIdAsync(id);
        if (lead == null)
        {
            _logger.LogWarning("Lead no encontrado con ID {Id}", id);
            return NotFound(new { message = "Lead no encontrado." });
        }
        return Ok(lead);
    }
}
