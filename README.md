# ISOCodex.Addressing

`ISOCodex.Addressing` is a .NET library for modelling, formatting, and validating postal addresses against country-specific rules.

## Projects

- `src/Addressing` - core types, DI registration, built-in formatters, and built-in validators
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
using ISOCodex.Addressing.Formatting;
using ISOCodex.Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAddressing(
    CountryCode.GB,
    CountryCode.US,
    CountryCode.CA);

using var serviceProvider = services.BuildServiceProvider();

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();
var formatter = serviceProvider.GetRequiredService<IAddressFormatter>();

var address = new Address(
    line1: "10 Downing Street",
    line2: null,
    city: "London",
    stateOrProvince: null,
    postalCode: new PostalCode("SW1A 2AA"),
    countryCode: CountryCode.GB);

var validationResult = validatorFactory
    .GetValidator(address.CountryCode)
    .Validate(address);

var formatted = formatter.Format(address);
```

`formatted` contains a postal-friendly, country-specific layout:

```text
10 Downing Street
London
SW1A 2AA
United Kingdom
```

## Address formatting

Formatting is routed by `Address.CountryCode`. Register the countries your app supports, then ask `IAddressFormatter` to produce display or output text for each address.

The built-in formatter handles:

- country-specific line ordering
- optional second address lines
- postal code placement
- English country display names
- multi-line and single-line output

For compact UI, logs, CSV exports, or search results, request a single-line format:

```csharp
var singleLine = formatter.Format(
    address,
    new AddressFormatOptions
    {
        Style = AddressFormatStyle.SingleLine
    });
```

Output:

```text
10 Downing Street, London, SW1A 2AA, United Kingdom
```

If the country is already obvious from surrounding UI, you can omit it:

```csharp
var withoutCountry = formatter.Format(
    address,
    new AddressFormatOptions
    {
        IncludeCountry = false
    });
```

Formatting is presentation only. It does not validate the address, normalize the stored postal code, or prove the address exists. Use the validator for country-specific validation before formatting when correctness matters.

## Structured validation results

`Validate(...)` returns structured, form/API-friendly errors instead of throwing for ordinary validation failures:

```csharp
var result = validatorFactory
    .GetValidator(address.CountryCode)
    .Validate(address);

if (!result.IsValid)
{
    foreach (var issue in result.Issues)
    {
        Console.WriteLine($"{issue.PropertyName}: {issue.Message}");
    }
}
```

Each issue includes a stable `Code`, a human-readable `Message`, and an optional `PropertyName`.

## Built-in countries

- Great Britain (`GB`)
- United States (`US`)
- Canada (`CA`)

## Spain extension

Spain support is delivered by the separate `ISOCodex.Addressing.Spain` package.

```bash
dotnet add package ISOCodex.Addressing.Spain
```

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
```

## Release focus

Package identity, namespaces, NuGet metadata, and package documentation should stay aligned under the `ISOCodex.Addressing` name.

## License

MIT
