using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Pages
{
	public class HomeBase : ComponentBase
	{
		protected SortedDictionary<int, RollDto> rolls;
		protected SortedDictionary<int, RollDto> rolls2;
		protected SortedDictionary<int, FrameDto> frames;

		protected string Message { get; set; }

		protected override void OnInitialized()
		{
			Message = "Are you ready to bowl the night away?";
			rolls = new()
			{
				{1, new RollDto{KnockedPins = 1}},
				{2, new RollDto{KnockedPins = 2}}
			};
			rolls2 = new()
			{
				{1, new RollDto{KnockedPins = 1}},
				{2, new RollDto{KnockedPins = 2}},
				{3, new RollDto{KnockedPins = 3}}
			};
			frames = new()
			{
				{1, new FrameDto{Rolls=@rolls, MaxRolls=2}},
				{2, new FrameDto{Rolls=@rolls, MaxRolls = 2}},
				{3, new FrameDto{Rolls=@rolls, MaxRolls = 2}},
				{4, new FrameDto{Rolls=@rolls, MaxRolls = 2}},
				{5, new FrameDto{Rolls=@rolls, MaxRolls=2}},
				{6, new FrameDto{Rolls=@rolls, MaxRolls=2}},
				{7, new FrameDto{Rolls=@rolls, MaxRolls=2}},
				{8, new FrameDto{Rolls=@rolls, MaxRolls=2}},
				{9, new FrameDto{MaxRolls=2}},
				{10, new FrameDto{Rolls=@rolls2, MaxRolls=3}},
			};
			base.OnInitialized();
		}

		protected void StartGame()
		{
			Message = "Lets go!";
			frames = new()
			{
				{1, new FrameDto{Rolls=@rolls}},
				{2, new FrameDto{Rolls=@rolls}},
				{3, new FrameDto{Rolls=@rolls}},
				{4, new FrameDto{Rolls=@rolls}},
				{5, new FrameDto{Rolls=@rolls}},
				{6, new FrameDto{Rolls=@rolls}},
				{7, new FrameDto{Rolls=@rolls}},
				{8, new FrameDto{Rolls=@rolls}},
				{9, new FrameDto{Rolls=@rolls}},
				{10, new FrameDto{Rolls=@rolls}},
			};
		}
	}
}
