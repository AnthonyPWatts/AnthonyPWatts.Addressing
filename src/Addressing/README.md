# Addressing

A domain model for international addresses and ISO 3166-1 country codes.

## Features
- Support for ISO 3166-1 country codes.
- Address validation and formatting.
- Extensible design for country-specific validators.

## Installation
Install via NuGet:
```bash
dotnet add package Addressing
```

## Usage
```csharp
using Addressing;
using Addressing.Validation;

// Create an address
var address = new Address( "123 Main St", null, "Madrid", null, new PostalCode("28001", CountryCode.Parse("ES")), CountryCode.Parse("ES"));

// Validate the address
var validatorFactory = new DefaultAddressValidatorFactory();
validatorFactory.RegisterValidator(CountryCode.Parse("ES"), new SpanishAddressValidator());

var validator = validatorFactory.GetValidator(address.CountryCode); validator.Validate(address);
Console.WriteLine("Address is valid!");


## License
This project is licensed under the MIT License. See the `LICENSE` file for details.
