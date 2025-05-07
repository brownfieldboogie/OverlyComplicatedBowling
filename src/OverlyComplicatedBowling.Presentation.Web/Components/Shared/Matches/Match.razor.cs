using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Presentation.Web.Services;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared.Matches
{
	public class MatchBase : ComponentBase
	{
		[Inject] public IOverlyComplicatedBowlingService OverlyComplicatedBowlingService { get; set; }
		[Parameter] public MatchDto Match { get; set; }
        protected bool IsRolling { get; set; }

        protected async Task AddRoll(Guid matchId, Guid gameId)
		{
            if (IsRolling) return;

            try
            {
                IsRolling = true;
                var updatedMatch = await OverlyComplicatedBowlingService.AddRollAsync(matchId, gameId);

                Match.IdOfActiveGame = updatedMatch.IdOfActiveGame;
                Match.Games.Clear();
                Match.Games.AddRange(updatedMatch.Games);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                IsRolling = false;
                StateHasChanged();
            }
        }
	}
}
