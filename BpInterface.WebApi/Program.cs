using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using BpInterface.Infrastructure.Data;
using BpInterface.Infrastructure.Data.Entities;
using BpInterface.Infrastructure.Repositories;
using BpInterface.Infrastructure.Validations;
using BpInterface.Services.Implementations;
using BpInterface.WebApi.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuring Services

builder.Services.AddDbContext<BpContext>(c =>
                c.UseSqlServer(builder.Configuration.GetConnectionString("BpDataBaseConnection")));

builder.Services.AddScoped<IDbRepository<Cuenta>, BpRepository<Cuenta>>();
builder.Services.AddScoped<IDbRepository<Cliente>, BpRepository<Cliente>>();
builder.Services.AddScoped<IDbRepository<Movimiento>, BpRepository<Movimiento>>();

builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ICuentaService, CuentaService>();
builder.Services.AddTransient<IMovimientoService, MovimientoService>();

//validations
builder.Services.AddFluentValidationAutoValidation(opt => opt.DisableDataAnnotationsValidation = true);
builder.Services.AddTransient<IValidator<ClienteRequestForUpdate>, ClienteRequestForUpdateValidator>();
builder.Services.AddTransient<IValidator<ClienteRequestForCreate>, ClienteRequestForCreateValidator>();
builder.Services.AddTransient<IValidator<CuentaRequestForCreate>, CuentaRequestForCreateValidator>();
builder.Services.AddTransient<IValidator<CuentaRequestForUpdate>, CuentaRequestForUpdateValidator>();
builder.Services.AddTransient<IValidator<MovimientoRequestForCreate>, MovimientoRequestForCreateValidator>();
builder.Services.AddTransient<IValidator<MovimientoSearchParams>, MovimientoSearchParamsValidator>();

#endregion

var app = builder.Build();

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<BpContext>();
        await BpContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Logger.LogInformation("End seeding Database...");


// Configure the HTTP request pipeline.
// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

//For Testing purpose
public partial class Program { }