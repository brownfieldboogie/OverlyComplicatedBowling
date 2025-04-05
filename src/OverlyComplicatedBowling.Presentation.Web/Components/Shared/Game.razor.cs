using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared
{
	public class GameBase : ComponentBase
	{
		[Parameter]
		public List<FrameDto> Frames { get; set; }
	}
}
