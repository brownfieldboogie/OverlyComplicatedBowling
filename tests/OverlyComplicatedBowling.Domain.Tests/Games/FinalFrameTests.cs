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
            var rollOne = 6;
            var rollTwo = 4;
            var rollThree = 1;
            var rollFour = 1;

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
            var rollOne = 10;
            var rollTwo = 4;
            var rollThree = 1;

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
            var rollOne = 6;
            var rollTwo = 4;
            var rollThree = 1;

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
            var rollOne = 3;
            var rollTwo = 4;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_RemainingPinsShouldBe10MinusLastRoll()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 1;

            //Act
            frame.AddRoll(rollOne);

            //Assert
            frame.RemainingPins.Should().Be(10 - rollOne);
        }

        [TestMethod]
        public void AddRoll_RemainingPinsShouldBe10IfStrike()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 10;

            //Act
            frame.AddRoll(rollOne);

            //Assert
            frame.RemainingPins.Should().Be(10);
        }

        [TestMethod]
        public void AddRoll_RemainingPinsShouldBe10IfSpare()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 1;
            var rollTwo = 9;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.RemainingPins.Should().Be(10);
        }

        [TestMethod]
        public void UpdateScore_Rolls_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 3;
            var rollTwo = 4;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne + rollTwo);
        }

        [TestMethod]
        public void UpdateScore_Spare_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 6;
            var rollTwo = 4;
            var rollThree = 4;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne + rollTwo + rollThree);
        }

        [TestMethod]
        public void UpdateScore_Strike_SumAllKnockedPins()
        {
            //Arrange
            var frame = FinalFrame.Create();
            var rollOne = 10;
            var rollTwo = 10;
            var rollThree = 10;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);
            frame.UpdateScore();

            //Assert
            frame.Rolls.Should().HaveCount(3);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne + rollTwo + rollThree);
        }
    }
}
