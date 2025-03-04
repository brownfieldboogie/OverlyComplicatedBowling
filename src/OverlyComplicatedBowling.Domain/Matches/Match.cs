using OverlyComplicatedBowling.Domain.Games;
using System.Collections.ObjectModel;

namespace OverlyComplicatedBowling.Domain.Matches
{
	public class Match
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		internal SortedDictionary<int, Game> _games = [];
		internal int _keyOfActiveGame = 0;
		public IReadOnlyDictionary<int, Game> Games => new ReadOnlyDictionary<int, Game>(_games);

		public static Match Start(int numberOfPlayers)
		{
			var match = new Match();
			match.CreateGames(numberOfPlayers);
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
	}
}
