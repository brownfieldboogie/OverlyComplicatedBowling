using OverlyComplicatedBowling.Domain.Games;
using System.Collections.ObjectModel;

namespace OverlyComplicatedBowling.Domain.Matches
{
	public class Match
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		internal SortedDictionary<int, Game> _games = [];
		public Guid IdOfActiveGame;
		public IReadOnlyDictionary<int, Game> Games => new ReadOnlyDictionary<int, Game>(_games);

		public static Match Start(int numberOfPlayers)
		{
			var match = new Match();
			match.CreateGames(numberOfPlayers);
			match.IdOfActiveGame = match.Games.First().Value.Id;
			return match;
		}

		private void CreateGames(int numberOfPlayers)
		{
			for (int i = 0; i < numberOfPlayers; i++)
			{
				Game game = Game.Start();
				_games.Add(i, game);
			}
		}

		public void AddRoll(int knockedPins)
		{
			_games[GetKeyOfActiveGame()].AddRoll(knockedPins);
			IdOfActiveGame = _games[GetKeyOfActiveGame()].Id;
		}

		public int GetKeyOfActiveGame()
		{
			return _games.Aggregate((g1, g2) => g1.Value.GetKeyOfActiveFrame() <= g2.Value.GetKeyOfActiveFrame() ? g1 : g2).Key;
		}
	}
}
