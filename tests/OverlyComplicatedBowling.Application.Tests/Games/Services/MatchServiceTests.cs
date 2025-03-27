using FluentAssertions;
using NSubstitute;
using OverlyComplicatedBowling.Application.Games.Services;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Matches;

namespace OverlyComplicatedBowling.Application.Tests.Games.Services
{
	[TestClass]
	public class MatchServiceTests
	{
		[TestMethod]
		public async Task StartMatchAsync_ReturnFreshMatch()
		{
			//Arrange
			var matchService = new MatchService(Substitute.For<IBowlingRollWebservice>(), Substitute.For<IMatchRepository>());

			//Act
			var matchDto = await matchService.StartMatchAsync(1);

			//Assert
			matchDto.Games.First().Value.Frames.Should().HaveCount(10);
			matchDto.Games.First().Value.Frames.Values.Should().AllSatisfy(f => f.Rolls.Values.Should().BeEmpty());
			matchDto.Games.First().Value.Frames.Values.Should().AllSatisfy(f => f.Completed.Should().BeFalse());
		}

		[TestMethod]
		public async Task AddRollAsync_AddsRollToActiveFrameOnActiveGame()
		{
			//Arrange
			var bowlingRollWebservice = Substitute.For<IBowlingRollWebservice>();
			bowlingRollWebservice.GetRollResultAsync(default).ReturnsForAnyArgs(10);
			var match = Match.Start(1);
			var matchRepository = Substitute.For<IMatchRepository>();
			matchRepository.LoadMatchAsync(default).ReturnsForAnyArgs(match);

			var matchService = new MatchService(bowlingRollWebservice, matchRepository);

			//Act
			var updatedMatchDto = await matchService.AddRollAsync(Guid.NewGuid());

			//Assert
			updatedMatchDto.Games.First().Value.Frames.First().Value.Completed.Should().BeTrue();
			updatedMatchDto.Games.First().Value.Frames.First().Value.Rolls.Values.First().IsStrike.Should().BeTrue();
		}
	}
}
