using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Infrastructure.Repositories.MatchRepository;
using OverlyComplicatedBowling.Infrastructure.Scraping;

namespace OverlyComplicatedBowling.Infrastructure
{
	public static class DependecyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IMatchRepository, MatchRepository>();
			services.AddTransient<IBowlingRollWebservice, BowlingRollWebservice>();
			services.AddDbContext<MatchDbContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
			});

			return services;
		}
	}
}
