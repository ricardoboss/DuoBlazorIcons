using Microsoft.AspNetCore.Components;

namespace DuoBlazorIcons.Components;

public abstract class BaseIcon : ComponentBase
{
	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object>? AdditionalAttributes { get; set; }

	protected Dictionary<string, object> RootElementAttributes
	{
		get
		{
			var attributes = AdditionalAttributes ?? new Dictionary<string, object>();

			return attributes;
		}
	}
}
