using FluentAssertions;
using OverlyComplicatedBowling.Application.Games.Mapping;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Tests.Games.Mapping
{
	[TestClass]
	public class GameDtoMapperTests
	{
		[TestMethod]
		public void MapDto_MapsGameToGameDto()
		{
			//Arrange
			var game = Game.Start(0);
			var rollValue = 3;
			game.AddRoll(rollValue);
			game.AddRoll(rollValue);

			//Act
			var gameDto = GameDtoMapper.MapDto(game);

			//Assert
			gameDto.Id.Should().Be(game.Id);
			gameDto.Frames.First().Rolls.Count.Should().Be(game.Frames.First().Rolls.Count);
			gameDto.GameCompleted.Should().Be(game.IsGameCompleted());
			gameDto.TotalScore.Should().Be(rollValue * 2);
		}
	}
}
