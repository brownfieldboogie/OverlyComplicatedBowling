using OverlyComplicatedBowling.Application.Games.Dtos;
using OverlyComplicatedBowling.Domain.Games;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
    public static class GameDtoMapper
    {
        public static GameDto MapDto(Game game) => new()
        {
            Id = game.Id,
            Frames = game.Frames.ToDictionary(pair => pair.Key, pair => FrameDtoMapper.MapDto(pair.Value)),
            GameCompleted = game.IsGameCompleted()
        };
    }
}
