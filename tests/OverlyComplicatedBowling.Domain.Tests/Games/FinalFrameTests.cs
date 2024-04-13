using FluentAssertions;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Domain.Tests.Games
{
    [TestClass]
    public class FinalFrameTests
    {
        [TestMethod]
        public void AddRoll_AddNoMoreThanThreeRolls()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(3, false, false);
            var rollTwo = new Roll(4, false, false);
            var rollThree = new Roll(1, false, false);
            var rollFour = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);
            frame.AddRoll(rollFour);

            //Assert
            frame.Rolls.Should().HaveCount(3);
        }
    }
}
