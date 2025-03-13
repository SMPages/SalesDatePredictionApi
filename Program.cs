using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Acceder al sistema de logs
var logger = builder.Logging; 
// Configurar logging en consola y depurador
logger.ClearProviders();
logger.AddConsole();
logger.AddDebug();

try
{
    // Obtener la cadena de conexión desde appsettings.json
    //Variables de entorno para publicar: 
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("ERROR: La cadena de conexión no está configurada en appsettings.json.");
    }
    // Inyectar la conexión a la base de datos
    builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

    // Inyección de dependencias para el repositorio
    builder.Services.AddSingleton<IRepository, Repository>();

    // Configuración de CORS (para que Angular pueda consumir la API)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            policy => policy.WithOrigins("http://localhost:4200") // Solo permite peticiones desde Angular
                            .AllowAnyMethod()
                            .AllowAnyHeader());
    });

    // Agregar controladores
    builder.Services.AddControllers();

    // Configurar Swagger para la documentación de la API
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    // Leer configuración desde appsettings.json
    var urls = builder.Configuration["Kestrel:Endpoints:Http:Url"];
    if (!string.IsNullOrEmpty(urls))
    {
        app.Urls.Add(urls);
    }

    // Middleware para logs y errores
    var appLogger = app.Services.GetRequiredService<ILogger<Program>>();

    // Habilitar Swagger solo en desarrollo
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Usar CORS con la política configurada
    app.UseCors("AllowFrontend");

    app.UseAuthorization();

    app.MapControllers();

    appLogger.LogInformation("API iniciada en {Url}", "http://localhost:5281");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error al iniciar la aplicación: {ex.Message}");
    throw;
}
