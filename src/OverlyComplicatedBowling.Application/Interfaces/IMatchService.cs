using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Application.Interfaces
{
	public interface IMatchService
	{
		Task<MatchDto> StartMatchAsync(int numberOfPlayers);
		Task<MatchDto> AddRollAsync(Guid matchId);
	}
}
