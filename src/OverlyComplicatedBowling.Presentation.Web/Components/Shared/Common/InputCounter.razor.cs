using Microsoft.AspNetCore.Components;

namespace OverlyComplicatedBowling.Presentation.Web.Components.Shared.Common
{
	public class InputCounterBase : ComponentBase
	{
		[Parameter] public string Message { get; set; }
		[Parameter] public int Value { get; set; }
		[Parameter] public int MinValue { get; set; } = 0;
		[Parameter] public int DefaultValue { get; set; } = 0;
		[Parameter] public EventCallback<int> ValueChanged { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Value = DefaultValue;
		}

		protected async Task Increase()
		{
			Value++;
			await ValueChanged.InvokeAsync(Value);
		}

		protected async Task Decrease()
		{
			if (Value == MinValue) return;

			Value--;
			await ValueChanged.InvokeAsync(Value);
		}
	}
}
