using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Repositories;
using tecnom_technical_test.Mappings;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LeadDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Challenge Técnico: Back-End Dev (NET) - API Technical tests",
        Version = "v1",
        Description = "API para gestionar servicios",
        Contact = new OpenApiContact
        {
            Name = "Víctor Hugo Contreras",
            Email = "vcontreras@gmail.com"
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("LeadsDb"));


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
    cfg.AddProfile(new LeadProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Registro de servicios para Leads y Places
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IPlacesService, PlacesService>();
builder.Services.AddMemoryCache();

// Ejemplo de resiliencia con Polly (comentado porque en este entorno no está disponible)
/*
builder.Services.AddHttpClient("EPlacesApiClient")
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt))
    );
*/


// Registrar EPlacesService usando patrón de fábrica y el cliente nombrado
builder.Services.AddHttpClient("EPlacesApiClient");
builder.Services.AddScoped<IEPlacesService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("EPlacesApiClient");
    var cache = sp.GetRequiredService<IMemoryCache>();
    var logger = sp.GetRequiredService<ILogger<EPlacesService>>();
    var config = sp.GetRequiredService<IConfiguration>();
    return new EPlacesService(httpClient, cache, logger, config);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Technical API v1");
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


app.UseHttpsRedirection();

app.Run();

