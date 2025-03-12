using FluentAssertions;
using OverlyComplicatedBowling.Domain.Matches;

namespace OverlyComplicatedBowling.Domain.Tests.Matches
{
	[TestClass]
	public class MatchTests
	{
		[TestMethod]
		public void Match_Start_CreatesMatchWithMultipleGames()
		{
			//Arrange
			var numberOfPlayers = 2;

			//Act
			var match = Match.Start(numberOfPlayers);

			//Assert
			match.Games.Should().HaveCount(numberOfPlayers);
			match.Games.Values.Select(g => g.Id).Should().OnlyHaveUniqueItems();
			match.IdOfActiveGame.Should().Be(match.Games.First().Value.Id);
		}

		[TestMethod]
		public void Match_GetKeyOfActiveGame_CanCalculateKeyOfActiveGame()
		{
			//Arrange
			var match = Match.Start(2);
			match.AddRoll(1);

			//Act
			var keyOfActiveGame = match.GetKeyOfActiveGame();

			//Assert
			keyOfActiveGame.Should().Be(0);
		}

		[TestMethod]
		public void Match_GetKeyOfActiveGame_CanCalculateKeyOfActiveGameIfStrike()
		{
			//Arrange
			var match = Match.Start(2);
			match.AddRoll(10);

			//Act
			var keyOfActiveGame = match.GetKeyOfActiveGame();

			//Assert
			keyOfActiveGame.Should().Be(1);
		}

		[TestMethod]
		public void Match_AddRoll_AddsARollToTheActiveGame()
		{
			//Arrange
			var match = Match.Start(2);

			//Act
			match.AddRoll(1);

			//Assert
			match.IdOfActiveGame.Should().Be(match.Games.First().Value.Id);
		}
	}
}
