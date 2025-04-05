using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Domain.Matches
{
	public class Match
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public List<Game> Games { get; set; }
		public Guid IdOfActiveGame;

		public static Match Start(int numberOfPlayers)
		{
			var match = new Match();
			match.Games = [];
			match.CreateGames(numberOfPlayers);
			match.IdOfActiveGame = match.Games.First().Id;
			return match;
		}

		private void CreateGames(int numberOfPlayers)
		{
			for (int i = 0; i < numberOfPlayers; i++)
			{
				Game game = Game.Start(i);
				Games.Add(game);
			}
		}

		public void AddRoll(int knockedPins)
		{
			Games[GetIndexOfActiveGame()].AddRoll(knockedPins);
			IdOfActiveGame = Games[GetIndexOfActiveGame()].Id;
		}

		public Game GetActiveGame()
		{
			return Games[GetIndexOfActiveGame()];
		}

		public int GetIndexOfActiveGame()
		{
			return Games.Aggregate((g1, g2) => g1.GetIndexOfActiveFrame() <= g2.GetIndexOfActiveFrame() ? g1 : g2).Index;
		}
	}
}
