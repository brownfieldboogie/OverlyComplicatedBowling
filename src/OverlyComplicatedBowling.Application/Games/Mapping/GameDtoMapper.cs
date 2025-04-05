using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class GameDtoMapper
	{
		public static GameDto MapDto(Game game) => new()
		{
			Id = game.Id,
			Frames = [.. game.Frames.Select(FrameDtoMapper.MapDto)],
			GameCompleted = game.IsGameCompleted(),
			TotalScore = game.TotalScore
		};
	}
}
