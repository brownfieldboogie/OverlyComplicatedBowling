using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Presentation.Web.Services;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Pages
{
	public class HomeBase : ComponentBase
	{
		[Inject] public IOverlyComplicatedBowlingService OverlyComplicatedBowlingService { get; set; }

		protected List<FrameDto> Frames = [];
		protected MatchDto Match { get; set; }
		protected bool MatchStarted { get; set; }
		protected bool MatchCompleted { get; set; }
		protected string StartButtonText { get; set; }
		protected string Message { get; set; }
		protected int NumberOfPlayers { get; set; }

		protected override void OnInitialized()
		{
			Message = "Are you ready to bowl the night away?";
			StartButtonText = "Start game";
			NumberOfPlayers = 1;
			MatchStarted = false;
			MatchCompleted = false;
			base.OnInitialized();
		}

		protected async Task StartGame()
		{
			var newMatch = await OverlyComplicatedBowlingService.StartMatchAsync(NumberOfPlayers);
			Message = "Lets go!";
			StartButtonText = "New game!";
			MatchStarted = true;

			Match = new MatchDto
            {
                Id = newMatch.Id,
                IdOfActiveGame = newMatch.IdOfActiveGame,
                Games = newMatch.Games
            };
        }
	}
}
