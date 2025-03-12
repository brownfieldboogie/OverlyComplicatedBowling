using OverlyComplicatedBowling.Domain.Matches;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class MatchDtoMapper
	{
		public static MatchDto MapDto(Match match) => new()
		{
			Id = match.Id,
			Games = new SortedDictionary<int, GameDto>(match.Games.ToDictionary(pair => pair.Key, pair => GameDtoMapper.MapDto(pair.Value))),
			IdOfActiveGame = match.IdOfActiveGame
		};
	}
}
