using OverlyComplicatedBowling.Application.Games.Dtos;

namespace OverlyComplicatedBowling.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> StartGameAsync();
        Task<GameDto> AddRollAsync(Guid gameId);
    }
}
