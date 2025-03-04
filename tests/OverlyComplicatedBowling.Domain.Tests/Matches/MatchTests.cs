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
		}
	}
}
