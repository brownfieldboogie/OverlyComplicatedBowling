using Microsoft.Extensions.DependencyInjection;
using OverlyComplicatedBowling.Application.Games.Services;
using OverlyComplicatedBowling.Application.Interfaces;

namespace OverlyComplicatedBowling.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddTransient<IMatchService, MatchService>();

			return services;
		}
	}
}
