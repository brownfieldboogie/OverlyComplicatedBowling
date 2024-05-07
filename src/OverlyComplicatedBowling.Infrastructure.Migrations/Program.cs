using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OverlyComplicatedBowling.Infrastructure.Repositories;

//To add migration use "dotnet ef migrations add NAME"

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<PostgreSQLDbContext>(options =>
    {
        options.UseNpgsql(context.Configuration.GetConnectionString("PostgreSQL"), o =>
            o.MigrationsAssembly("OverlyComplicatedBowling.Infrastructure.Migrations")); //could be more graceful
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PostgreSQLDbContext>();
    db.Database.Migrate();
}

app.Run(); //todo shut down automatically when database has been updated
