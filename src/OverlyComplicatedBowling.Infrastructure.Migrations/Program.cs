using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OverlyComplicatedBowling.Infrastructure.Repositories;
using System.Reflection;

//To add migration use "dotnet ef migrations add NAME"

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices((context, services) =>
{
    var connectionString = context.Configuration.GetConnectionString("PostgreSQL");
    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

    services.AddDbContext<PostgreSQLDbContext>(options =>
    {
        options.UseNpgsql(connectionString, o => o.MigrationsAssembly(assemblyName));
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostgreSQLDbContext>();
    db.Database.Migrate();
}

app.Run(); //todo shut down automatically when database has been updated
