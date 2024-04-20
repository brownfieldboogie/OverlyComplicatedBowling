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
            var rollOne = new Roll(6, false, false);
            var rollTwo = new Roll(4, false, true);
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

        [TestMethod]
        public void AddRoll_CompleteFrameAfterThreeRollsIfStrike()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(10, true, false);
            var rollTwo = new Roll(4, false, false);
            var rollThree = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_CompleteFrameAfterThreeRollsIfSpare()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(6, false, false);
            var rollTwo = new Roll(4, false, true);
            var rollThree = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_CompleteFrameAfterTwoRollsIfNoStrikeOrSpare()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(3, false, false);
            var rollTwo = new Roll(4, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void UpdateScore_Rolls_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(3, false, false);
            var rollTwo = new Roll(4, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne.KnockedPins + rollTwo.KnockedPins);
        }

        [TestMethod]
        public void UpdateScore_Spare_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(6, false, false);
            var rollTwo = new Roll(4, false, true);
            var rollThree = new Roll(4, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne.KnockedPins + rollTwo.KnockedPins + rollThree.KnockedPins);
        }

        [TestMethod]
        public void UpdateScore_Strike_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = new Roll(10, true, false);
            var rollTwo = new Roll(10, true, false);
            var rollThree = new Roll(10, true, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne.KnockedPins + rollTwo.KnockedPins + rollThree.KnockedPins);
        }
    }
}
