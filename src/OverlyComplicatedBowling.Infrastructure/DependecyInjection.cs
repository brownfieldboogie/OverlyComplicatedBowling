using Microsoft.Extensions.DependencyInjection;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Infrastructure.Repositories;
using OverlyComplicatedBowling.Infrastructure.Scraping;

namespace OverlyComplicatedBowling.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, InMemoryGameRepository>(); //todo move to actual db so we dont need singleton

            services.AddTransient<IBowlingRollWebservice, BowlingRollWebservice>();

            return services;
        }
    }
}
