using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class GameDtoMapper
	{
		public static GameDto MapDto(Game game) => new()
		{
			Id = game.Id,
			Frames = new SortedDictionary<int, FrameDto>(game.Frames.ToDictionary(pair => pair.Key, pair => FrameDtoMapper.MapDto(pair.Value))),
			GameCompleted = game.IsGameCompleted(),
			TotalScore = game.TotalScore
		};
	}
}
