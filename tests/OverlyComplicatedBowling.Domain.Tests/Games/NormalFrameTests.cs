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
            var rollOne = new Roll(3, false, false);
            var rollTwo = new Roll(4, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Rolls.First().Value.KnockedPins.Should().Be(rollOne.KnockedPins);
            frame.Rolls.Last().Value.KnockedPins.Should().Be(rollTwo.KnockedPins);
        }

        [TestMethod]
        public void AddRoll_AddNoMoreThanTwoRolls()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = new Roll(3, false, false);
            var rollTwo = new Roll(4, false, false);
            var rollThree = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.AddRoll(rollThree);

            //Assert
            frame.Rolls.Should().HaveCount(2);
        }

        [TestMethod]
        public void UpdateScore_Rolls_SumRollsAsScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
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
        public void UpdateScore_Spare_UseNextRollInScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = new Roll(5, false, false);
            var rollTwo = new Roll(5, false, true);
            var subsequentRoll = new Roll(5, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.AddRoll(rollTwo);
            frame.UpdateScore([subsequentRoll]);

            //Assert
            frame.Rolls.Should().HaveCount(2);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne.KnockedPins + rollTwo.KnockedPins + subsequentRoll.KnockedPins);
        }

        [TestMethod]
        public void UpdateScore_Strike_UseNextTwoRollsInScore()
        {
            //Arrange
            var frame = NormalFrame.Create();
            var rollOne = new Roll(10, true, false);
            var subsequentRollOne = new Roll(5, false, false);
            var subsequentRollTwo = new Roll(1, false, false);

            //Act
            frame.AddRoll(rollOne);
            frame.UpdateScore([subsequentRollOne, subsequentRollTwo]);

            //Assert
            frame.Rolls.Should().HaveCount(1);
            frame.Scored.Should().BeTrue();
            frame.Score.Should().Be(rollOne.KnockedPins + subsequentRollOne.KnockedPins + subsequentRollTwo.KnockedPins);
        }
    }
}
