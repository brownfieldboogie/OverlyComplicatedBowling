using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Services
{
	public interface IOverlyComplicatedBowlingService
	{
		Task<GameDto> StartGameAsync();
		Task<GameDto> AddRollAsync(Guid gameId);
	}
}
