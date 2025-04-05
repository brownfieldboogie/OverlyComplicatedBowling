using OverlyComplicatedBowling.Domain.Matches;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class MatchDtoMapper
	{
		public static MatchDto MapDto(Match match) => new()
		{
			Id = match.Id,
			Games = [.. match.Games.Select(GameDtoMapper.MapDto)],
			IdOfActiveGame = match.IdOfActiveGame
		};
	}
}
