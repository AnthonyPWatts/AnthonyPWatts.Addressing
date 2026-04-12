# AnthonyPWatts.Addressing

`AnthonyPWatts.Addressing` is a .NET library for modelling postal addresses and validating them using country-specific rules.

The solution currently contains:

- a core library for address modelling and validator registration
- built-in validators for Great Britain, the United States, and Canada
- a Spain extension project
- automated tests
- a small manual test rig

## What this library does

The core package provides:

- `Address` as a compact address model
- `CountryCode` as an ISO 3166-1 alpha-2 value object
- `PostalCode` as a country-aware postal code value object
- `IAddressValidator` for country-specific validation
- `IAddressValidatorFactory` for resolving validators by country
- `AddAddressing(...)` for DI registration of selected built-in validators

## Solution structure

- `src/Addressing`  
  Core library and NuGet package source

- `src/Addressing.Spain`  
  Spain-specific validator extension

- `tests/Addressing.Tests`  
  xUnit tests for value objects, factory behaviour, DI registration, and validator behaviour

- `ManualTestRig`  
  Small console app for ad hoc manual experimentation

## Built-in validator coverage

The core package currently supports:

- `GB`
- `US`
- `CA`

Spain support exists in the separate `Addressing.Spain` project.

## Installation

For the core package:

```bash
dotnet add package APW.Addressing
```

## Quick start

```csharp
using Addressing;
using Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing(
    CountryCode.Parse("GB"),
    CountryCode.Parse("US"),
    CountryCode.Parse("CA"));

using var serviceProvider = services.BuildServiceProvider();

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var address = new Address(
    line1: "1600 Pennsylvania Avenue NW",
    line2: null,
    city: "Washington",
    stateOrProvince: "DC",
    postalCode: new PostalCode("20500", CountryCode.Parse("US")),
    countryCode: CountryCode.Parse("US"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Spain extension

Spain support lives in a separate project.

```csharp
using Addressing;
using Addressing.Spain;
using Addressing.Utilities;
using Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing(CountryCode.Parse("GB"), CountryCode.Parse("US"));
services.AddSpainAddressing();

using var serviceProvider = services.BuildServiceProvider();

foreach (var startupAction in serviceProvider.GetServices<IStartupAction>())
{
    startupAction.Execute();
}

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var address = new Address(
    line1: "Calle de Alcalá, 45",
    line2: null,
    city: "Madrid",
    stateOrProvince: "Madrid",
    postalCode: new PostalCode("28014", CountryCode.Parse("ES")),
    countryCode: CountryCode.Parse("ES"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Notes on current behaviour

A few design points are worth knowing up front:

- `AddAddressing(...)` only registers the built-in validators you ask for
- requesting an unsupported country through `AddAddressing(...)` currently results in an exception when the factory is resolved
- `PostalCode` does not validate format on construction; format validation happens in country validators
- US addresses currently require `StateOrProvince`
- Canada allows province/territory to be omitted, but validates it when supplied
- UK postcode validation currently expects uppercase postcodes with a space

## Status

This project is in active development and is being prepared for public NuGet use. Expect the API and package set to continue to evolve.

## License

MIT. See `LICENSE.txt`.
