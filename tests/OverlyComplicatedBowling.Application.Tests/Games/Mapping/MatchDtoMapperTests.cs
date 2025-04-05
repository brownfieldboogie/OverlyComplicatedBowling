using FluentAssertions;
using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Domain.Matches;

namespace OverlyComplicatedBowling.Application.Tests.Games.Mapping
{
	[TestClass]
	public class MatchDtoMapperTests
	{
		[TestMethod]
		public void MapDto_MapsMatchToMatchDto()
		{
			//Arrange
			var match = Match.Start(2);
			match.AddRoll(10);

			//Act
			var matchDto = MatchDtoMapper.MapDto(match);

			//Assert
			matchDto.Id.Should().Be(match.Id);
			matchDto.Games.First().Frames.First().RemainingPins.Should().Be(0);
			matchDto.IdOfActiveGame.Should().Be(matchDto.Games[1].Id);
		}
	}
}
