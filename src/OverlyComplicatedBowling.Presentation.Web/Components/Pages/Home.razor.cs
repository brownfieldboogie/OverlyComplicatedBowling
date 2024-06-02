using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Presentation.Web.Services;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Pages
{
	public class HomeBase : ComponentBase
	{
		[Inject]
		public IOverlyComplicatedBowlingService OverlyComplicatedBowlingService { get; set; }

		protected SortedDictionary<int, FrameDto> Frames;
		private Guid _gameId;

		protected string Message { get; set; }

		protected override void OnInitialized()
		{
			Message = "Are you ready to bowl the night away?";
			Frames = new()
			{
				{1, new FrameDto{MaxRolls=2}},
				{2, new FrameDto{MaxRolls=2}},
				{3, new FrameDto{MaxRolls=2}},
				{4, new FrameDto{MaxRolls=2}},
				{5, new FrameDto{MaxRolls=2}},
				{6, new FrameDto{MaxRolls=2}},
				{7, new FrameDto{MaxRolls=2}},
				{8, new FrameDto{MaxRolls=2}},
				{9, new FrameDto{MaxRolls=2}},
				{10, new FrameDto{MaxRolls=3}},
			};
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
