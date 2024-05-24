using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared
{
	public class FrameBase : ComponentBase
	{
		[Parameter]
		public SortedDictionary<int, RollDto> Rolls { get; set; }

		[Parameter]
		public int MaxRolls { get; set; }
	}
}
