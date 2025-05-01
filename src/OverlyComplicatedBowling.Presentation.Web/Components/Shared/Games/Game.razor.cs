using Microsoft.AspNetCore.Components;
using OverlyComplicatedBowling.Shared.Dtos;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared.Games
{
	public class GameBase : ComponentBase
	{
		[Parameter] public List<FrameDto> Frames { get; set; }
	}
}
