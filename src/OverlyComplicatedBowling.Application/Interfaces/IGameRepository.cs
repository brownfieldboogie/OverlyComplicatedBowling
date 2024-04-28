using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Interfaces
{
    public interface IGameRepository
    {
        void SaveGame(Game game);
        Game? LoadGame(Guid Id);
    }
}
