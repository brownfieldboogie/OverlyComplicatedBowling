using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Presentation.Web.Services;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Pages
{
	public class HomeBase : ComponentBase
	{
		[Inject]
		public IOverlyComplicatedBowlingService OverlyComplicatedBowlingService { get; set; }

		protected List<FrameDto> Frames = [];
		private Guid _gameId;
		protected bool GameStarted { get; set; }
		protected bool GameCompleted { get; set; }
		protected string StartButtonText { get; set; }

		protected string Message { get; set; }

		protected override void OnInitialized()
		{
			Message = "Are you ready to bowl the night away?";
			StartButtonText = "Start game";
			base.OnInitialized();
		}

		protected async Task StartGame()
		{
			var newGame = await OverlyComplicatedBowlingService.StartGameAsync();
			Message = "Lets go!";
			StartButtonText = "Restart game!";

			Frames = newGame.Frames;
			_gameId = newGame.Id;
			GameStarted = true;
			GameCompleted = newGame.GameCompleted;
		}

		protected async Task AddRoll()
		{
			var updatedGame = await OverlyComplicatedBowlingService.AddRollAsync(_gameId);

			Frames = updatedGame.Frames;
			GameCompleted = updatedGame.GameCompleted;
		}
	}
}
