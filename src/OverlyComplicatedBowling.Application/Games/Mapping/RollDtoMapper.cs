using OverlyComplicatedBowling.Application.Games.Dtos;
using OverlyComplicatedBowling.Domain.Games;

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
