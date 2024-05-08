using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Infrastructure.Serialization;

namespace OverlyComplicatedBowling.Infrastructure.Repositories
{
    public class PostgreSQLDbContext : DbContext
    {
        public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .Property(g => g.Frames)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, JsonSettings.TypeNameHandlingAuto),
                    v => JsonConvert.DeserializeObject<SortedDictionary<int, Frame>>(v, JsonSettings.TypeNameHandlingAuto));
        }

        public DbSet<Game> Games { get; set; }
    }
}
