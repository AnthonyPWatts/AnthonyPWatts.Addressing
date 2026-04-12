# ISOCodex.Addressing

A domain model for international addresses and ISO 3166-1 country codes.

## Features

- ISO 3166-1 alpha-2 country code value object
- postal code value object
- address aggregate
- extensible validator factory
- built-in validators for GB, US, and CA

## Installation

```bash
dotnet add package ISOCodex.Addressing
```

## Usage

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
    "1600 Pennsylvania Avenue NW",
    null,
    "Washington",
    "DC",
    new PostalCode("20500", CountryCode.Parse("US")),
    CountryCode.Parse("US"));

validatorFactory.GetValidator(address.CountryCode).Validate(address);
```

## Extension packages

Spain support lives in the separate `ISOCodex.Addressing.Spain` package.

## License

This project is licensed under the MIT License. See `LICENSE` for details.
