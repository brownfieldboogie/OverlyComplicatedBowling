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
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/StartMatch", async (IMatchService matchService, int numberOfPlayers) =>
{
	return Results.Ok(await matchService.StartMatchAsync(numberOfPlayers));
})
.WithName("StartMatch").WithOpenApi();

app.MapPost("/AddRoll/{matchId}/{gameId}", async (IMatchService matchService, Guid matchId, Guid gameId) =>
{
	return await matchService.AddRollAsync(matchId, gameId) is { } updatedMatch ? Results.Ok(updatedMatch) : Results.NotFound();
})
.WithName("AddRoll").WithOpenApi();

app.Run();
