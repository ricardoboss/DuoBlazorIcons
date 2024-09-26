using Microsoft.AspNetCore.Components;

namespace DuoBlazorIcons.Components;

public abstract class BaseIcon : ComponentBase
{
	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object>? AdditionalAttributes { get; set; }

	[Parameter]
	public decimal? PrimaryOpacity { get; set; }

	[Parameter]
	public decimal? SecondaryOpacity { get; set; }

	protected Dictionary<string, object> RootElementAttributes
	{
		get
		{
			const string iconClassName = "duo-blazor-icon";
			var attributes = new Dictionary<string, object>
			{
				{ "width", "1em" },
				{ "height", "1em" },
				{ "class", iconClassName },
			};

			if (AdditionalAttributes is { } additionalAttributes)
			{
				foreach (var attribute in additionalAttributes)
				{
					attributes[attribute.Key] = attribute.Value;
				}
			}

			if (!(attributes["class"] as string)!.Contains(iconClassName))
			{
				attributes["class"] = $"{iconClassName} {attributes["class"]}";
			}

			attributes["viewbox"] = "0 0 24 24";
			attributes["xmlns"] = "http://www.w3.org/2000/svg";

			return attributes;
		}
	}
}
