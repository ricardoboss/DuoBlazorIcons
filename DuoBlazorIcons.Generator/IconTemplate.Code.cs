namespace DuoBlazorIcons.Generator;

public partial class IconTemplate(IconMetadata metadata)
{
	private static string GetOpacityExpression(SvgPath path)
	{
		var opacityOverridePropertyName = path.Layer switch
		{
			PathLayer.Primary => "PrimaryOpacity",
			PathLayer.Secondary => "SecondaryOpacity",
			_ => null,
		};

		var fallbackOpacity = path.Opacity ?? 1m;

		return $"@({opacityOverridePropertyName} ?? {fallbackOpacity}m)";
	}
}
