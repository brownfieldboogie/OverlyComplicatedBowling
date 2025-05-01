using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Services
{
	public interface IOverlyComplicatedBowlingService
	{
		Task<MatchDto> StartMatchAsync(int numberOfPlayers);
		Task<MatchDto> AddRollAsync(Guid matchId, Guid gameId);
	}
}
