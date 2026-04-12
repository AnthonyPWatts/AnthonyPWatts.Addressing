# ISOCodex.Addressing

`ISOCodex.Addressing` provides a small .NET domain model for postal addresses plus country-specific address validation through a validator factory.

## Features

- ISO 3166-1 alpha-2 country code value object
- Lightweight postal code value object
- Address model suitable for application/domain use
- Country-specific validation via `IAddressValidator`
- DI registration for selected built-in validators
- Built-in support for:
  - Great Britain (`GB`)
  - United States (`US`)
  - Canada (`CA`)

## Installation

```bash
dotnet add package ISOCodex.Addressing
```

## Core types

- `Address`
- `CountryCode`
- `PostalCode`
- `IAddressValidator`
- `IAddressValidatorFactory`

## Register validators with DI

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
```

## Validate an address

```csharp
using ISOCodex.Addressing;

var address = new Address(
    line1: "10 Downing Street",
    line2: null,
    city: "London",
    stateOrProvince: null,
    postalCode: new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
    countryCode: CountryCode.Parse("GB"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Country-specific notes

### Great Britain (`GB`)

- Validates postcode format
- Expects uppercase postcode format with a space, for example `SW1A 1AA`

### United States (`US`)

- Validates ZIP or ZIP+4
- Requires `StateOrProvince`
- Accepts US state and territory abbreviations

### Canada (`CA`)

- Validates postal code format such as `A1A 1A1`
- Province/territory is optional
- If provided, province/territory must be a valid abbreviation

## Extension packages

Country support can be extended through additional packages. Spain support is provided by the separate `ISOCodex.Addressing.Spain` project in the repository rather than by this core package.

## Important behaviour

- `PostalCode` itself does not enforce country-specific formatting
- Validation is performed by the country validator
- `AddAddressing(...)` only registers the validators you explicitly request

## License

MIT
