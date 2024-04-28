using Microsoft.Extensions.Logging;
using OverlyComplicatedBowling.Application.Interfaces;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Infrastructure.Repositories
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<Guid, Game> _games = [];
        private readonly ILogger _logger;

        public InMemoryGameRepository(ILogger logger)
        {
            _logger = logger;
        }

        public void SaveGame(Game game)
        {
            if (_games.ContainsKey(game.Id))
            {
                _games[game.Id] = game;
            }
            else
            {
                _games.Add(game.Id, game);
            }
        }

        public Game? LoadGame(Guid Id)
        {
            if (_games.TryGetValue(Id, out Game game))
            {
                return game;
            }
            else
            {
                _logger.LogWarning("Did not find game with id {id}, returning null", Id);
                return null;
            }
        }
    }
}
