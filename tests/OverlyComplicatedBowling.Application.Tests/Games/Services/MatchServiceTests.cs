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
			matchDto.Games.First().Frames.Should().HaveCount(10);
			matchDto.Games.First().Frames.Should().AllSatisfy(f => f.Rolls.Values.Should().BeEmpty());
			matchDto.Games.First().Frames.Should().AllSatisfy(f => f.Completed.Should().BeFalse());
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
			var updatedMatchDto = await matchService.AddRollAsync(match.Id, match.Games.First().Id);

			//Assert
			updatedMatchDto.Games.First().Frames.First().Completed.Should().BeTrue();
			updatedMatchDto.Games.First().Frames.First().Rolls.Values.First().IsStrike.Should().BeTrue();
		}
	}
}
