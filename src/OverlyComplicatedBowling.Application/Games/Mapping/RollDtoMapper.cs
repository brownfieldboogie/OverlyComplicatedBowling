using OverlyComplicatedBowling.Domain.Games;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Games.Mapping
{
	public static class RollDtoMapper
	{
		public static RollDto MapDto(Roll roll) => new()
		{
			KnockedPins = roll.KnockedPins,
			IsStrike = roll.IsStrike,
			IsSpare = roll.IsSpare,
		};
	}
}
