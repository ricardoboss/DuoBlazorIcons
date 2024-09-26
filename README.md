# DuoBlazorIcons

Duo Blazor Icons is a set of multi-tone icons for Blazor.

![Full Icon Set](https://raw.githubusercontent.com/ricardoboss/DuoBlazorIcons/main/.github/assets/full-icon-set.png)

## Installation

Install the package from NuGet:

```shell
dotnet add package DuoBlazorIcons
```

## Usage

To use the icons, you can use the `Icon` component:

```razor
@using DuoBlazorIcons.Components

<Icon Name="IconName.AddCircle" />
```

Or you can use the icon component directly:

```razor
@using DuoBlazorIcons.Components.Icons

<IconAddCircle />
```

## Development

Then icon components and the `IconName` enum are not versioned by git since they are purely generated code.

To generate/update the icon components, you need to run the `DuoBlazorIcons.Generator` project:

```shell
dotnet run --project DuoBlazorIcons.Generator/DuoBlazorIcons.Generator.csproj
```

This will generate the `IconName` enum and the `Icon<icon name>` components.

### Contributing

Contributions are welcome! Please open an issue or a pull request if you have any suggestions or improvements.

New icons should be added to [fazdiu/duo-icons](https://github.com/fazdiu/duo-icons) first, so open a PR there and then open a PR here to add the icon
to the generator.

## License

Duo Blazor Icons is licensed under the [MIT License](LICENSE.md).

### Attribution

The icons are based on the [fazdiu/duo-icons](https://github.com/fazdiu/duo-icons) project licensed under the [MIT License](https://github.com/fazdiu/duo-icons/blob/master/LICENSE).
