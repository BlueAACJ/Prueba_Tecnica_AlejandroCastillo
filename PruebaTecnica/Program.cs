using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Services;
using PruebaTecnica.Database;

// AExtensiones de SQLite
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using PruebaTecnica.Services.Implementation;
using PruebaTecnica.Models;

var builder = WebApplication.CreateBuilder(args);

// Conexion a la base de datos
builder.Services.AddDbContext<BaseDatos>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConexionSQLite")));

// Lista de servicios
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

builder.Services.AddControllers().AddJsonOptions(opciones =>
{
    opciones.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.s
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
