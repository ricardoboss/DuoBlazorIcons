using System.Globalization;
using System.Xml;
using DuoBlazorIcons.Generator;

var iconDirectory = Path.Combine(Directory.GetCurrentDirectory(), "icons");

var outputProjectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "DuoBlazorIcons");
var componentsOutputDirectory = Path.Combine(outputProjectDirectory, "Components");
Directory.CreateDirectory(componentsOutputDirectory);
var generatedIconOutputDirectory = Path.Combine(componentsOutputDirectory, "Icons");
Directory.CreateDirectory(generatedIconOutputDirectory);

var iconFiles = Directory.GetFiles(iconDirectory, "*.svg", SearchOption.AllDirectories);
var knownIcons = new List<IconMetadata>();
foreach (var iconFile in iconFiles)
{
	var iconName = GetIconName(iconFile);
	var componentName = $"Icon{iconName}";

	Console.WriteLine($"Processing {iconName}");

	var paths = GetSvgPaths(iconFile).ToList();

	var metadata = new IconMetadata(
		Path.GetFileName(iconFile),
		iconName,
		componentName,
		paths
	);

	metadata = PreprocessMetadata(metadata);

	knownIcons.Add(metadata);

	var code = GenerateCode(metadata);

	var outputFile = Path.Combine(generatedIconOutputDirectory, $"{componentName}.razor");

	File.WriteAllText(outputFile, code);
}

Console.WriteLine("Generating icon name enum");

var enumCode = GenerateEnumCode(knownIcons);
var enumOutputFile = Path.Combine(componentsOutputDirectory, "IconName.cs");
File.WriteAllText(enumOutputFile, enumCode);

return;

static string GetIconName(string svgFile)
{
	var iconName = Path.GetFileNameWithoutExtension(svgFile);

	iconName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(iconName);
	iconName = iconName.Replace("-", "");

	return iconName;
}

static IEnumerable<SvgPath> GetSvgPaths(string svgFile)
{
	var svg = File.ReadAllText(svgFile);
	var svgDoc = new XmlDocument();
	svgDoc.LoadXml(svg);

	var namespaceManager = new XmlNamespaceManager(svgDoc.NameTable);
	namespaceManager.AddNamespace("svg", "http://www.w3.org/2000/svg");

	var paths = svgDoc.SelectNodes("//svg:path", namespaceManager)!;
	foreach (var path in paths.OfType<XmlElement>())
	{
		var pathData = path.Attributes["d"]?.Value;
		var className = path.Attributes["class"]?.Value;
		var opacityStr = path.Attributes["opacity"]?.Value;
		decimal? opacity = opacityStr == null ? null : decimal.Parse(opacityStr, CultureInfo.InvariantCulture);

		if (string.IsNullOrEmpty(pathData))
			throw new InvalidOperationException($"Icon {svgFile} has a path with no path data");

		PathLayer? layer = className switch
		{
			"duoicon-primary-layer" => PathLayer.Primary,
			"duoicon-secondary-layer" => PathLayer.Secondary,
			_ => null,
		};

		yield return new(pathData, layer, className, opacity);
	}
}

static IconMetadata PreprocessMetadata(IconMetadata metadata)
{
	return metadata with
	{
		Paths = PreprocessPaths(metadata.Paths).ToList(),
	};
}

static IEnumerable<SvgPath> PreprocessPaths(IEnumerable<SvgPath> paths)
{
	foreach (var path in paths)
	{
		var (pathData, layer, className, opacity) = path;

		var mappedClassName = layer switch
		{
			PathLayer.Primary => "primary-layer",
			PathLayer.Secondary => "secondary-layer",
			_ => className,
		};

		yield return new(pathData, layer, mappedClassName, opacity);
	}
}

static string GenerateCode(IconMetadata icon)
{
	var template = new IconTemplate(icon);

	return template.TransformText();
}

static string GenerateEnumCode(IEnumerable<IconMetadata> icons)
{
	var template = new IconNameEnumTemplate(icons);

	return template.TransformText();
}
