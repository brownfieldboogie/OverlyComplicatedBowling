using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Domain.Matches;
using OverlyComplicatedBowling.Infrastructure.Repositories.MatchRepository;

namespace OverlyComplicatedBowling.Infrastructure.Tests.Repositories
{
	[TestClass]
	public class PostgreSQLRepositoryTests
	{
		[TestMethod]
		public async Task SaveMatchAsync_SavesMatch()
		{
			//Arrange
			var dbContextOptions = new DbContextOptionsBuilder<MatchDbContext>()
				.UseInMemoryDatabase("mockdb")
				.Options;
			var dbContext = new MatchDbContext(dbContextOptions);
			var repository = new MatchRepository(dbContext);
			var match = Match.Start(1);

			//Act
			await repository.SaveMatchAsync(match);
			var result = await repository.LoadMatchAsync(match.Id);

			//Assert
			result.Should().NotBeNull();
		}

		[TestMethod]
		public async Task SavematchAsync_SavesGames()
		{
			//Arrange
			var dbContextOptions = new DbContextOptionsBuilder<MatchDbContext>()
				.UseInMemoryDatabase("mockdb")
				.Options;
			var dbContext = new MatchDbContext(dbContextOptions);
			var repository = new MatchRepository(dbContext);
			var match = Match.Start(1);

			//Act
			await repository.SaveMatchAsync(match);
			var result = await repository.LoadMatchAsync(match.Id);

			//Assert
			result.Should().NotBeNull();
			var resultGame = result.Games.First().Value;
			resultGame.Frames.Should().NotBeNull();
			resultGame.Frames.First().Key.Should().Be(1);
			resultGame.Frames.First().Value.GetType().Should().Be(typeof(NormalFrame));
			resultGame.Frames.Last().Key.Should().Be(10);
			resultGame.Frames.Last().Value.GetType().Should().Be(typeof(FinalFrame));
		}

		[TestMethod]
		public async Task SaveMatchAsync_MapsNormalFramesCorrectly()
		{
			//Arrange
			var dbContextOptions = new DbContextOptionsBuilder<MatchDbContext>()
				.UseInMemoryDatabase("mockdb")
				.Options;
			var dbContext = new MatchDbContext(dbContextOptions);
			var repository = new MatchRepository(dbContext);
			var match = Match.Start(1);
			match.AddRoll(1);
			match.AddRoll(2);

			//Act
			await repository.SaveMatchAsync(match);
			var result = await repository.LoadMatchAsync(match.Id);

			//Assert
			result.Should().NotBeNull();
			var resultGame = result.Games.First().Value;
			resultGame.Frames.Should().NotBeNull();
			resultGame.Frames.First().Should().NotBeNull();
			resultGame.Frames.First().Value.Should().BeEquivalentTo(match.Games.First().Value.Frames.First().Value);
		}
	}
}
