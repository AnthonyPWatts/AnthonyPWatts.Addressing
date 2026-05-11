# ISOCodex.Addressing

`ISOCodex.Addressing` is a .NET library for modelling postal addresses and validating them against country-specific rules.

> [!NOTE]
> This library is in the process of being standardised under the **ISOCodex** name.
>
> The intended long-term package and namespace direction is **`ISOCodex.Addressing`**.
> The current repository name, **`AnthonyPWatts.Addressing`**, reflects the original project location and may change as the package is prepared for release.
>
> Until the first stable NuGet release, package IDs, namespaces and repository names should be treated as pre-release and subject to change. The public API and naming will be settled before any stable package is published.


## Projects

- `src/Addressing` - core types, DI registration, and built-in validators
- `src/Addressing.Spain` - Spain extension package
- `tests/Addressing.Tests` - unit and integration-style tests
- `ManualTestRig` - small console app for quick manual smoke testing

## Package identity

- Core package: `ISOCodex.Addressing`
- Spain extension package: `ISOCodex.Addressing.Spain`
- Root namespaces: `ISOCodex.Addressing*`

## Installation

```bash
dotnet add package ISOCodex.Addressing
```

## Quick start

```csharp
using ISOCodex.Addressing;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing(
    CountryCode.Parse("GB"),
    CountryCode.Parse("US"),
    CountryCode.Parse("CA"));

using var serviceProvider = services.BuildServiceProvider();

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var address = new Address(
    line1: "10 Downing Street",
    line2: null,
    city: "London",
    stateOrProvince: null,
    postalCode: new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
    countryCode: CountryCode.Parse("GB"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Built-in validators

- Great Britain (`GB`)
- United States (`US`)
- Canada (`CA`)

## Spain extension

The Spain validator is delivered by the separate `ISOCodex.Addressing.Spain` package.

```bash
dotnet add package ISOCodex.Addressing.Spain
```

```csharp
using ISOCodex.Addressing;
using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Utilities;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing();
services.AddSpainAddressing();

using var serviceProvider = services.BuildServiceProvider();

foreach (var startupAction in serviceProvider.GetServices<IStartupAction>())
{
    startupAction.Execute();
}

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
```

## Release focus

The current highest-value release task is to keep package identity, namespaces, NuGet metadata, and package documentation perfectly aligned. Consumers should never see `APW.Addressing`, `Addressing`, and `ISOCodex.Addressing` used interchangeably for the same core package.

## License

MIT
