using FluentAssertions;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Domain.Tests.Games
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Game_Start_CreatesGameWith9FreshNormalFramesAnd1FreshFinalFrame()
        {
            //Act
            var game = Game.Start();

            //Assert
            game.Frames.Should().HaveCount(10);
            game.Frames.Keys.Should().ContainInOrder(Enumerable.Range(1, 10).ToList());
            game.Frames.Values.Take(9).Should().AllBeOfType(typeof(NormalFrame));
            game.Frames.Values.TakeLast(1).Should().AllBeOfType(typeof(FinalFrame));
            game.Frames.Values.Should().AllSatisfy(f => f.Scored.Should().BeFalse());
            game.Frames.Values.Should().AllSatisfy(f => f.Score.Should().Be(0));
            game.Frames.Values.Should().AllSatisfy(f => f.Rolls.Values.Should().BeEmpty());
        }

        [TestMethod]
        public void GetRemainingPinsOnActiveFrame_ReturnRemainingPins()
        {
            //Arrange
            var game = Game.Start();

            //Act
            var remainingPins = game.GetRemainingPinsOnActiveFrame();

            //Assert
            remainingPins.Should().Be(10);
        }
    }
}
