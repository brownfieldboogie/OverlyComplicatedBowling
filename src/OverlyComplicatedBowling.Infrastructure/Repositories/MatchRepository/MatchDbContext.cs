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

			modelBuilder.Entity<Match>(match =>
			{
				match.HasKey(m => m.Id);

				match.HasMany<Game>().WithOne().HasForeignKey("MatchId").OnDelete(DeleteBehavior.Cascade);

				match.Property(m => m.IdOfActiveGame);
			});

			modelBuilder.Entity<Game>(game =>
			{
				game.HasKey(g => g.Id);

				game.Property(g => g.Frames)
					.HasConversion(
						v => JsonConvert.SerializeObject(v, JsonSettings.TypeNameHandlingAuto),
						v => JsonConvert.DeserializeObject<List<Frame>>(v, JsonSettings.TypeNameHandlingAuto));

				game.Property(g => g.TotalScore);
				game.Property(g => g.Index);
			});
		}

		public DbSet<Match> Matches { get; set; }
		public DbSet<Game> Games { get; set; }
	}
}
