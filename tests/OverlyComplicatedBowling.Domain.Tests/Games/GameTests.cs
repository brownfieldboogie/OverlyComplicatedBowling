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
		public void IsGameCompleted_AllFramesNotCompleted_ReturnsFalse()
		{
			//Arrange
			var game = Game.Start();
			game.AddRoll(10);
			game.AddRoll(10);

			//Act
			var gameCompleted = game.IsGameCompleted();

			//Assert
			gameCompleted.Should().BeFalse();
		}

		[TestMethod]
		public void IsGameCompleted_AllFramesCompleted_ReturnsTrue()
		{
			//Arrange
			var game = Game.Start();
			for (int i = 0; i < 12; i++)
			{
				game.AddRoll(10);
			}

			//Act
			var gameCompleted = game.IsGameCompleted();

			//Assert
			gameCompleted.Should().BeTrue();
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

		[TestMethod]
		public void AddRoll_FinalFrameThreeRolls_AddRollAndUpdateScore()
		{
			//Arrange
			var game = Game.Start();
			var knockedPins = 10;

			//Act
			for (int i = 0; i < 12; i++)
			{
				game.AddRoll(knockedPins);
			}

			//Assert
			game.Frames[10].Rolls.Count.Should().Be(3);
			game.Frames[10].Scored.Should().BeTrue();
		}

		[TestMethod]
		public void AddRoll_FinalFrameTwoRolls_AddRollAndUpdateScore()
		{
			//Arrange
			var game = Game.Start();

			//Act
			for (int i = 0; i < 9; i++)
			{
				game.AddRoll(10);
			}
			game.AddRoll(1);
			game.AddRoll(1);

			//Assert
			game.Frames[10].Rolls.Count.Should().Be(2);
			game.Frames[10].Scored.Should().BeTrue();
		}

		[TestMethod]
		public void AddRoll_PerfectGame_AccumulatedScoreOnLastFrameShouldBe300()
		{
			//Arrange
			var game = Game.Start();
			var knockedPins = 10;

			//Act
			for (int i = 0; i < 12; i++)
			{
				game.AddRoll(knockedPins);
			}

			//Assert
			game.Frames.Last().Value.AccumulatedScore.Should().Be(300);
		}

		[TestMethod]
		public void AddRoll_PerfectGame_TotalScoreShouldBe300()
		{
			//Arrange
			var game = Game.Start();
			var knockedPins = 10;

			//Act
			for (int i = 0; i < 12; i++)
			{
				game.AddRoll(knockedPins);
			}

			//Assert
			game.TotalScore.Should().Be(300);
		}
	}
}
