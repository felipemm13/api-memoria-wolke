using APIWolke.Models;
using APIWolke.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.IO; // Asegúrate de tener este using para usar Path y Directory

var builder = WebApplication.CreateBuilder(args);

// Carga las variables de entorno desde .env
var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
MyDotEnv.Load(envPath); // Usa tu clase personalizada para cargar variables del archivo .env

// Configura los ajustes de MongoDB
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();

// Política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configura los controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");

// Añadir el middleware de autenticación API Key directamente, no como un servicio
app.UseMiddleware<ApiKeyMiddleware>();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
