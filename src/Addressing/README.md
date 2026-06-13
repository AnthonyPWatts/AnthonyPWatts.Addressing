# ISOCodex.Addressing

`ISOCodex.Addressing` provides a small .NET domain model for postal addresses plus country-specific formatting and validation.

## Features

- ISO 3166-1 alpha-2 country code value object
- Lightweight postal code value object
- Address model suitable for application/domain use
- Country-specific address formatting
- Country-specific validation via `IAddressValidator`
- DI registration for selected built-in formatters and validators
- Opt-in generic fallbacks for valid ISO countries without country packs
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
- `IAddressFormatter`
- `AddressValidationResult`
- `AddressValidationIssue`
- `IAddressValidator`
- `IAddressValidatorFactory`

## Register country services with DI

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
```

`AddAddressing(...)` registers both the formatter and validator for each requested built-in country.

## Validate an address

```csharp
using ISOCodex.Addressing;

var address = new Address(
    line1: "10 Downing Street",
    line2: null,
    city: "London",
    stateOrProvince: null,
    postalCode: new PostalCode("SW1A 2AA"),
    countryCode: CountryCode.GB);

var result = validatorFactory
    .GetValidator(address.CountryCode)
    .Validate(address);

if (!result.IsValid)
{
    foreach (var issue in result.Issues)
    {
        Console.WriteLine(issue.Code);
        Console.WriteLine(issue.PropertyName);
        Console.WriteLine(issue.Message);
    }
}
```

`Validate(...)` does not throw for ordinary validation failures. It returns structured issue data for APIs, forms, imports, and other user-facing workflows.

`AddressValidationResult` contains:

- `IsValid` - `true` when no issues were found
- `Issues` - a list of `AddressValidationIssue`

Each `AddressValidationIssue` contains:

- `Code` - stable machine-readable issue code, for example `Address.PostalCode.Invalid`
- `Message` - human-readable description
- `PropertyName` - related `Address` property when applicable

Built-in validators collect multiple issues where possible.

## Format an address

```csharp
using ISOCodex.Addressing.Formatting;

var formatted = formatter.Format(address);
```

Default output is multi-line and includes the country. For a GB address:

```text
10 Downing Street
London
SW1A 2AA
United Kingdom
```

The formatter uses `Address.CountryCode` to choose the correct country-specific formatter. That lets application code stay simple while each formatter owns its country's postal layout.

The built-in countries currently format as:

```text
GB
10 Downing Street
London
SW1A 2AA
United Kingdom

US
1600 Pennsylvania Avenue NW
Washington, DC 20500
United States

CA
111 Wellington Street
Ottawa ON K1A 0A9
Canada
```

Country names are currently emitted as English display names: `United Kingdom`, `United States`, `Canada`, and country names supplied by extension packages. Localization and alternate country-name output can be added later without changing the core routing model.

### Single-line formatting

Single-line output is useful for grids, autocomplete results, logs, exports, and other compact displays:

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

You can also choose a different separator:

```csharp
var pipeSeparated = formatter.Format(
    address,
    new AddressFormatOptions
    {
        Style = AddressFormatStyle.SingleLine,
        SingleLineSeparator = " | "
    });
```

`AddressFormatOptions` is mutable by design for straightforward call-site configuration:

```csharp
var options = new AddressFormatOptions
{
    Style = AddressFormatStyle.SingleLine,
    IncludeCountry = false
};
```

### Omitting the country

When the country is already shown elsewhere in your UI or data export, omit the country line:

```csharp
var localOnly = formatter.Format(
    address,
    new AddressFormatOptions
    {
        IncludeCountry = false
    });
```

Output:

```text
10 Downing Street
London
SW1A 2AA
```

### Custom country formatters

Extension packages and applications can register additional country formatters:

```csharp
services.AddAddressFormatter(
    CountryCode.ES,
    () => new MySpanishAddressFormatter());
```

Custom formatters implement `ICountryAddressFormatter`. `IAddressFormatter` remains the single service consumers use to format any registered country.

## Unsupported countries and generic fallbacks

`CountryCode` can represent any valid ISO 3166-1 alpha-2 country code. That lets consumers store the country accurately even before this package has country-specific rules for it.

Country-specific services are still explicit by default. If no formatter or validator is registered for an address's country, `IAddressFormatter.Format(...)` and `IAddressValidatorFactory.GetValidator(...)` throw. This is the right behaviour when an application expects strict, country-pack-backed handling.

When an application needs to accept structured addresses for countries without a country pack, register generic fallbacks:

```csharp
services.AddAddressing(CountryCode.GB);
services.AddGenericAddressingFallbacks();
```

Registered country formatters and validators always take precedence. For unregistered countries, `AddGenericAddressingFallbacks()` registers:

- `PermissiveAddressValidator` - returns success for any non-null `Address`
- `GenericAddressFormatter` - emits `Line1`, optional `Line2`, `City StateOrProvince PostalCode`, and the ISO country code

Example output for a country without a specific formatter:

```text
1 Rue de Rivoli
Paris 75001
FR
```

This is intended for store-first, import, migration, and validate-later workflows. It does not prove that an address exists or is deliverable.

Generic fallbacks do not make `Address` freeform. The core model still requires `Line1`, `City`, `PostalCode`, and `CountryCode`. If your application must preserve addresses that do not fit that structure, store the raw address text separately in your own data model.

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
- Formatting is performed by the country formatter registered for `Address.CountryCode`
- Formatting does not mutate the address
- Formatting does not validate the address or prove that it exists
- Built-in country names are currently English display names
- Validators normalize common postal-code casing and spacing for validation without changing the stored `PostalCode.Code`
- `AddAddressing(...)` only registers the built-in countries you explicitly request
- `AddGenericAddressingFallbacks()` is opt-in and keeps registered country-specific services ahead of generic behaviour

## License

MIT
