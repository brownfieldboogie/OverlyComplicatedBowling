using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Infrastructure.Repositories;
using OverlyComplicatedBowling.Infrastructure.Scraping;

namespace OverlyComplicatedBowling.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGameRepository, PostgreSQLRepository>();
            services.AddTransient<IBowlingRollWebservice, BowlingRollWebservice>();
            services.AddDbContext<PostgreSQLDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"), o =>
                    o.MigrationsAssembly("OverlyComplicatedBowling.Infrastructure.Migrations")); //could be more graceful
            });

            return services;
        }
    }
}
