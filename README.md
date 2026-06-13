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

## Unsupported countries and fallback behaviour

`CountryCode` accepts any valid ISO 3166-1 alpha-2 country code, but country-specific formatting and validation are only available when a formatter and validator have been registered for that country.

By default, unsupported countries remain explicit: `GetValidator(...)` and `Format(...)` throw when no country-specific service is registered. This helps applications catch missing country packs when strict validation is expected.

For applications that need to save and display structured addresses for countries without a country pack, register generic fallbacks:

```csharp
var services = new ServiceCollection();

services.AddAddressing(CountryCode.GB);
services.AddGenericAddressingFallbacks();
```

With these fallbacks:

- registered country packs are still used first
- unregistered ISO countries use `PermissiveAddressValidator`
- unregistered ISO countries use `GenericAddressFormatter`
- validation does not prove the address is deliverable

The fallback validator accepts any non-null `Address` instance. It is intended for store-first or validate-later workflows where the consuming application still wants a structured address object.

The fallback formatter emits the available structured fields in a generic order and uses the ISO country code as the country line:

```text
1 Rue de Rivoli
Paris 75001
FR
```

Fallbacks do not make the `Address` model fully freeform. `Address` still requires `Line1`, `City`, `PostalCode`, and `CountryCode`. If an application needs to store addresses that cannot fit that structure, it should keep a separate raw/freeform field in its own persistence model.

## Recommended persistence shape

For relational storage, persist the value objects as strings and keep constraints aligned with the `Address` model rather than with one country's postal rules.

| Column | Suggested type | Required | Notes |
| --- | --- | --- | --- |
| `Line1` | `nvarchar(200)` | Yes | First delivery/address line. |
| `Line2` | `nvarchar(200)` | No | Apartment, suite, building, organization, or other secondary line. |
| `City` | `nvarchar(100)` | Yes | Locality/town/city value used by the current `Address` model. |
| `StateOrProvince` | `nvarchar(100)` | No | Region, province, state, county, department, prefecture, or equivalent. |
| `PostalCode` | `nvarchar(32)` | Yes | Store the user's value; validators may normalize for checking without mutating this value. |
| `CountryCode` | `char(2)` | Yes | ISO 3166-1 alpha-2 code, stored uppercase. |

Recommended constraints:

- require `Line1`, `City`, `PostalCode`, and `CountryCode`
- allow `Line2` and `StateOrProvince` to be null
- constrain `CountryCode` to exactly two uppercase ASCII letters
- avoid country-specific postal-code constraints in the database
- use Unicode string columns for human-entered address fields

Example SQL shape:

```sql
Line1 nvarchar(200) not null,
Line2 nvarchar(200) null,
City nvarchar(100) not null,
StateOrProvince nvarchar(100) null,
PostalCode nvarchar(32) not null,
CountryCode char(2) not null
```

These lengths are practical defaults, not package-enforced limits. Applications with legacy imports, unusually long organization names, or strict partner schemas can choose wider columns without changing how the library works.

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
