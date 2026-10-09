using Scalar.AspNetCore;
using TicketsApi.Api.Middleware;
using TicketsApi.Application;
using TicketsApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Cada capa registra sus propios servicios
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();            // /openapi/v1.json (también sirve para importar en Postman)
    app.MapScalarApiReference(); // /scalar
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
