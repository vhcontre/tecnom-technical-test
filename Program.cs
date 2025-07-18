using FluentValidation;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Repositories;

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

builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

var mapperConfig = new MapperConfiguration(cfg => {
    cfg.AddProfile(new AutoMapperProfile());
    cfg.AddProfile(new LeadProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Registro de servicios para Leads y Places
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IPlacesService, PlacesService>();
builder.Services.AddScoped<IEPlacesService, EPlacesService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

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


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

