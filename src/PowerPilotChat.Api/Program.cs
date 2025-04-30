using ContosoAcai.Application.Extensions;
using ContosoAcai.Application.Orders;
using ContosoAcai.Application.Orders.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using PowerPilotChat.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/orders", async (
    [FromBody] CreateOrdersRequest request,
    [FromServices] OrderService service) => (await service.CreateAsync(request)).ToApiResponse());

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}