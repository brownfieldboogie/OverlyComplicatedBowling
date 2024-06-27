using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Presentation.Web.Services;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Pages
{
	public class HomeBase : ComponentBase
	{
		[Inject]
		public IOverlyComplicatedBowlingService OverlyComplicatedBowlingService { get; set; }

		protected SortedDictionary<int, FrameDto> Frames = new();
		private Guid _gameId;

		protected string Message { get; set; }

		protected override void OnInitialized()
		{
			Message = "Are you ready to bowl the night away?";
			base.OnInitialized();
		}

		protected async Task StartGame()
		{
			var newGame = await OverlyComplicatedBowlingService.StartGameAsync();
			Message = "Lets go!";

			Frames = newGame.Frames;
			_gameId = newGame.Id;
		}

		protected async Task AddRoll()
		{
			var updatedGame = await OverlyComplicatedBowlingService.AddRollAsync(_gameId);

			Frames = updatedGame.Frames;
		}
	}
}
