# ISOCodex.Addressing.Spain

`ISOCodex.Addressing.Spain` adds Spanish address validation and formatting support to the core `ISOCodex.Addressing` library.

## What it provides

- `SpanishAddressValidator`
- `SpanishAddressFormatter`
- `AddSpainAddressing()` DI extension

## Prerequisites

Register the core addressing services first.

## Example

```csharp
using ISOCodex.Addressing;
using ISOCodex.Addressing.Formatting;
using ISOCodex.Addressing.Spain;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing();
services.AddSpainAddressing();

using var serviceProvider = services.BuildServiceProvider();

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
var formatter = serviceProvider.GetRequiredService<IAddressFormatter>();

var address = new Address(
    line1: "Calle Mayor 1",
    line2: null,
    city: "Madrid",
    stateOrProvince: "Madrid",
    postalCode: new PostalCode("28013"),
    countryCode: CountryCode.ES);

validatorFactory.GetValidator(CountryCode.ES).Validate(address);

var formatted = formatter.Format(address);
```

Default formatted output:

```text
Calle Mayor 1
28013 Madrid
Spain
```

## Formatting behaviour

The Spanish formatter produces a postal-friendly layout using the core `IAddressFormatter` service. `AddSpainAddressing()` registers the Spanish formatter for `CountryCode.ES`, so consumers do not need to resolve Spain-specific services directly.

Multi-line output:

```text
Calle Mayor 1
28013 Madrid
Spain
```

Single-line output:

```csharp
var singleLine = formatter.Format(
    address,
    new AddressFormatOptions
    {
        Style = AddressFormatStyle.SingleLine
    });
```

```text
Calle Mayor 1, 28013 Madrid, Spain
```

Country output can be omitted when the country is already known:

```csharp
var localOnly = formatter.Format(
    address,
    new AddressFormatOptions
    {
        IncludeCountry = false
    });
```

```text
Calle Mayor 1
28013 Madrid
```

## Validation behaviour

The Spain validator currently:

- requires country code `ES`
- requires a 5-digit postal code
- validates province names when provided
- checks that the postal code prefix matches the supplied province when a province is supplied

## License

MIT
