using FluentAssertions;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Domain.Tests.Games
{
    [TestClass]
    public class NormalFrameTests
    {
        [TestMethod]
        public void AddRoll_AddRollsInOrder()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 3;
            var rollTwo = 4;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Rolls.First().Value.KnockedPins.Should().Be(rollOne);
            frame.Rolls.Last().Value.KnockedPins.Should().Be(rollTwo);
        }

        [TestMethod]
        public void AddRoll_AddNoMoreThanTwoRolls()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 3;
            var rollTwo = 4;
            var rollThree = 1;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);

            //Assert
            frame.Rolls.Should().HaveCount(2);
        }

        [TestMethod]
        public void AddRoll_CompleteFrameOnStrike()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 10;

            //Act
            frame.AddRoll(rollOne);

            //Assert
            frame.Rolls.Should().HaveCount(1);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_CompleteFrameOnSpareAndTwoRolls()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 9;
            var rollTwo = 1;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_CompleteFrameAfterTwoRolls()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 1;
            var rollTwo = 1;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Completed.Should().BeTrue();
        }

        [TestMethod]
        public void AddRoll_RemainingPinsShouldBe10MinusRolls()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 1;
            var rollTwo = 1;

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.RemainingPins.Should().Be(10 - rollOne - rollTwo);
        }

        [TestMethod]
        public void UpdateScore_Rolls_SumRollsAsScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
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
        public void UpdateScore_Spare_UseNextRollInScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 5;
            var rollTwo = 5;
            var subsequentRoll = new Roll(5, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.UpdateScore([subsequentRoll]);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne + rollTwo + subsequentRoll.KnockedPins);
        }

        [TestMethod]
        public void UpdateScore_Strike_UseNextTwoRollsInScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = 10;
            var subsequentRollOne = new Roll(5, false, false);
            var subsequentRollTwo = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.UpdateScore([subsequentRollOne, subsequentRollTwo]);

            //Assert
            frame.Rolls.Should().HaveCount(1);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne + subsequentRollOne.KnockedPins + subsequentRollTwo.KnockedPins);
        }
    }
}
