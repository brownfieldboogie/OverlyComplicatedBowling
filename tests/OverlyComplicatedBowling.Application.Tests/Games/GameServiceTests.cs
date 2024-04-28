using FluentAssertions;
using NSubstitute;
using OverlyComplicatedBowling.Application.Games;
using OverlyComplicatedBowling.Application.Interfaces;

namespace OverlyComplicatedBowling.Application.Tests.Games
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void StartGame_ReturnFreshGame()
        {
            //Arrange
            var service = new GameService(Substitute.For<IBowlingRollWebservice>(), Substitute.For<IGameRepository>());

            //Act
            var game = service.StartGame();

            //Assert
            game.Frames.Should().HaveCount(10);
            game.Frames.Values.Should().AllSatisfy(f => f.Rolls.Values.Should().BeEmpty());
            game.Frames.Values.Should().AllSatisfy(f => f.Completed.Should().BeFalse());
        }
    }
}
