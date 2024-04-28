using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Interfaces
{
    public interface IGameRepository
    {
        Task SaveGameAsync(Game game);
        Task<Game?> LoadGameAsync(Guid Id);
    }
}
