using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Domain.Matches;
using OverlyComplicatedBowling.Infrastructure.Serialization;

namespace OverlyComplicatedBowling.Infrastructure.Repositories.MatchRepository
{
	public class MatchDbContext : DbContext
	{
		public MatchDbContext(DbContextOptions<MatchDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Match>()
				.Property(g => g.Games)
				.HasConversion(
					v => JsonConvert.SerializeObject(v, JsonSettings.TypeNameHandlingAuto),
					v => JsonConvert.DeserializeObject<SortedDictionary<int, Game>>(v, JsonSettings.TypeNameHandlingAuto));
		}

		public DbSet<Match> Matches { get; set; }
	}
}
