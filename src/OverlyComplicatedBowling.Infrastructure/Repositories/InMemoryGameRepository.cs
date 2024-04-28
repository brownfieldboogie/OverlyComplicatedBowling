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

        public Task SaveGameAsync(Game game)
        {
            if (_games.ContainsKey(game.Id))
            {
                _games[game.Id] = game;
            }
            else
            {
                _games.Add(game.Id, game);
            }

            return Task.CompletedTask;
        }

        public Task<Game?> LoadGameAsync(Guid Id)
        {
            Game? game = _games.GetValueOrDefault(Id);

            if (game == null)
            {
                _logger.LogWarning("Did not find game with id {id}, returning null", Id);
            }

            return Task.FromResult(game);
        }
    }
}
