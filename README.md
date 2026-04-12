# ISOCodex.Addressing

`ISOCodex.Addressing` is a .NET library for modelling postal addresses and validating them against country-specific rules.

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
    "10 Downing St",
    null,
    "London",
    null,
    new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
    CountryCode.Parse("GB"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Spain extension

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

services.AddAddressing(CountryCode.Parse("GB"), CountryCode.Parse("US"));
services.AddSpainAddressing();

using var serviceProvider = services.BuildServiceProvider();

foreach (var action in serviceProvider.GetServices<IStartupAction>())
{
    action.Execute();
}

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
```

## Release readiness note

This rename pass intentionally standardises:
- NuGet package IDs
- root namespaces
- README examples
- solution and test project identity

The next pass after applying these files should be a build, test, pack, and README sanity check.
