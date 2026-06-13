# ISOCodex.Addressing.Spain

`ISOCodex.Addressing.Spain` adds Spanish address validation support to the core `ISOCodex.Addressing` library.

## What it provides

- `SpanishAddressValidator`
- `AddSpainAddressing()` DI extension

## Prerequisites

Register the core addressing services first.

## Example

```csharp
using ISOCodex.Addressing;
using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing();
services.AddSpainAddressing();

using var serviceProvider = services.BuildServiceProvider();

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var address = new Address(
    line1: "Calle Mayor 1",
    line2: null,
    city: "Madrid",
    stateOrProvince: "Madrid",
    postalCode: new PostalCode("28013"),
    countryCode: CountryCode.ES);

validatorFactory.GetValidator(CountryCode.ES).Validate(address);
```

## Validation behaviour

The Spain validator currently:

- requires country code `ES`
- requires a 5-digit postal code
- validates province names when provided
- checks that the postal code prefix matches the supplied province when a province is supplied

## License

MIT
