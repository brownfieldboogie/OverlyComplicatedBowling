using OverlyComplicatedBowling.Domain.Matches;

namespace OverlyComplicatedBowling.Application.Interfaces
{
	public interface IMatchRepository
	{
		Task SaveMatchAsync(Match match);
		Task<Match?> LoadMatchAsync(Guid Id);
	}
}
