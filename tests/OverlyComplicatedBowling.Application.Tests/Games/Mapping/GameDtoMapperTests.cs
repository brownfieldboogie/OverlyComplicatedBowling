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
            var game = Game.Start();
            game.AddRoll(3);

            //Act
            var gameDto = GameDtoMapper.MapDto(game);

            //Assert
            gameDto.Id.Should().Be(game.Id);
            gameDto.Frames.Values.First().Rolls.Count.Should().Be(game.Frames.Values.First().Rolls.Count);
            gameDto.GameCompleted.Should().Be(game.IsGameCompleted());
        }
    }
}
