using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OverlyComplicatedBowling.Infrastructure.Repositories.MatchRepository;
using System.Reflection;

//To add migration use "dotnet ef migrations add NAME"

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices((context, services) =>
{
	var connectionString = context.Configuration.GetConnectionString("PostgreSQL");
	var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

	services.AddDbContext<MatchDbContext>(options =>
	{
		options.UseNpgsql(connectionString, o => o.MigrationsAssembly(assemblyName));
	});
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<MatchDbContext>();
	await db.Database.MigrateAsync();
}

using (app)
{
	await app.StopAsync();
}