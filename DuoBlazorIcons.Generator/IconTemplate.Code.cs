namespace DuoBlazorIcons.Generator;

public partial class IconTemplate(IconMetadata metadata)
{
	private static string GetFillExpression(SvgPath path)
	{
		var colorOverridePropertyName = path.Layer switch
		{
			PathLayer.Primary => "PrimaryColor",
			PathLayer.Secondary => "SecondaryColor",
			_ => null,
		};

		var cssPropertyName = path.Layer switch
		{
			PathLayer.Primary => "--duo-blazor-icon-primary-color",
			PathLayer.Secondary => "--duo-blazor-icon-secondary-color",
			_ => "--duo-blazor-icon-default-color",
		};

		var cssVarExpression = $"var({cssPropertyName}, currentColor)";

		return $"@({colorOverridePropertyName} ?? \"{cssVarExpression}\")";
	}

	private static string GetOpacityExpression(SvgPath path)
	{
		var opacityOverridePropertyName = path.Layer switch
		{
			PathLayer.Primary => "PrimaryOpacity",
			PathLayer.Secondary => "SecondaryOpacity",
			_ => null,
		};

		var fallbackOpacity = path.Opacity ?? 1m;
		var cssPropertyName = path.Layer switch
		{
			PathLayer.Primary => "--duo-blazor-icon-primary-opacity",
			PathLayer.Secondary => "--duo-blazor-icon-secondary-opacity",
			_ => "--duo-blazor-icon-default-opacity",
		};

		var cssVarExpression = $"var({cssPropertyName}, {fallbackOpacity})";

		return $"@({opacityOverridePropertyName}?.ToString(CultureInfo.InvariantCulture) ?? \"{cssVarExpression}\")";
	}
}
