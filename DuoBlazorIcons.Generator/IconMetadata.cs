namespace DuoBlazorIcons.Generator;

public record IconMetadata(string FileName, string EnumCaseName, string ComponentName, IReadOnlyList<SvgPath> Paths);
