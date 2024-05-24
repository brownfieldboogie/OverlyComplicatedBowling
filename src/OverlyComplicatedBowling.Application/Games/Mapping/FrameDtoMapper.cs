using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class FrameDtoMapper
	{
		public static FrameDto MapDto(Frame frame) => new()
		{
			Rolls = new SortedDictionary<int, RollDto>(frame.Rolls.ToDictionary(pair => pair.Key, pair => RollDtoMapper.MapDto(pair.Value))),
			MaxRolls = frame.MaxRolls,
			Score = frame.Score,
			Scored = frame.Scored,
			Completed = frame.Completed,
			RemainingPins = frame.RemainingPins
		};
	}
}
