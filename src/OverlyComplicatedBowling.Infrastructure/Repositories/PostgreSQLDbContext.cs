using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Infrastructure.Repositories
{
    public class PostgreSQLDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PostgreSQLDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSQL"), options =>
                options.MigrationsAssembly("OverlyComplicatedBowling.Infrastructure.Migrations")); //could be more graceful
        }

        public DbSet<Game> Games { get; set; } //todo fix dbset mappings
        //todo add tests using testcontainer
    }
}
