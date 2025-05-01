using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared.Games
{
	public class FrameBase : ComponentBase
	{
		[Parameter] public SortedDictionary<int, RollDto> Rolls { get; set; }

		[Parameter] public int MaxRolls { get; set; }

		[Parameter] public bool Scored { get; set; }

		[Parameter] public int AccumulatedScore { get; set; }
	}
}
