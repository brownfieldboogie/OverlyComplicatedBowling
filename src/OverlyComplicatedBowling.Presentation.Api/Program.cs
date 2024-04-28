using OverlyComplicatedBowling.Application;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/StartGame", async (IGameService gameService) =>
{
    return Results.Ok(await gameService.StartGameAsync());
})
.WithName("StartGame").WithOpenApi();

app.MapPost("/AddRoll/{gameId}", async (IGameService gameService, Guid gameId) =>
{
    return await gameService.AddRollAsync(gameId) is { } updatedGame ? Results.Ok(updatedGame) : Results.NotFound();
})
.WithName("AddRoll").WithOpenApi();

app.Run();
