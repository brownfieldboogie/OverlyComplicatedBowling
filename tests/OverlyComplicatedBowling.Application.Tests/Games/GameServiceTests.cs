using FluentAssertions;
using NSubstitute;
using OverlyComplicatedBowling.Application.Games;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Tests.Games
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public async Task StartGameAsync_ReturnFreshGame()
        {
            //Arrange
            var gameService = new GameService(Substitute.For<IBowlingRollWebservice>(), Substitute.For<IGameRepository>());

            //Act
            var gameDto = await gameService.StartGameAsync();

            //Assert
            gameDto.Frames.Should().HaveCount(10);
            gameDto.Frames.Values.Should().AllSatisfy(f => f.Rolls.Values.Should().BeEmpty());
            gameDto.Frames.Values.Should().AllSatisfy(f => f.Completed.Should().BeFalse());
        }

        [TestMethod]
        public async Task AddRollAsync_AddsRollToActiveFrame()
        {
            //Arrange
            var bowlingRollWebservice = Substitute.For<IBowlingRollWebservice>();
            bowlingRollWebservice.GetRollResultAsync(default).ReturnsForAnyArgs(10);
            var game = Game.Start();
            var gameRepository = Substitute.For<IGameRepository>();
            gameRepository.LoadGameAsync(default).ReturnsForAnyArgs(game);

            var gameService = new GameService(bowlingRollWebservice, gameRepository);

            //Act
            var updatedGameDto = await gameService.AddRollAsync(Guid.NewGuid());

            //Assert
            updatedGameDto.Frames.First().Value.Completed.Should().BeTrue();
            updatedGameDto.Frames.First().Value.Rolls.Values.First().IsStrike.Should().BeTrue();
        }
    }
}
