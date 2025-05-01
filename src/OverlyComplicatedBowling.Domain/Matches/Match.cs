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
            var match = new Match
            {
                Games = []
            };
            match.CreateGames(numberOfPlayers);
			match.IdOfActiveGame = match.Games.First(g => g.Index == 0).Id;
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
			Games.First(g => g.Index == GetIndexOfActiveGame()).AddRoll(knockedPins);
			IdOfActiveGame = Games.First(g => g.Index == GetIndexOfActiveGame()).Id;
		}

		public int GetIndexOfActiveGame()
		{
			return Games.OrderBy(g => g.Index).Aggregate((g1, g2) => g1.GetIndexOfActiveFrame() <= g2.GetIndexOfActiveFrame() ? g1 : g2).Index;
		}
	}
}
