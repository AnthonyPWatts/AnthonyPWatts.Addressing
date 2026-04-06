# AnthonyPWatts.Addressing

`AnthonyPWatts.Addressing` appears to be a .NET library for modelling postal addresses and validating them against country-specific rules. The repository currently contains a core package, a Spain extension package, unit tests, and a small console-based manual test rig.

## Project aims

- Provide a compact domain model for postal addresses.
- Represent ISO 3166-1 alpha-2 country codes as a first-class type.
- Validate addresses through country-specific validators selected by country code.
- Allow the validator set to be extended through additional packages.

## Repository structure

- `src/Addressing` - Core library containing `Address`, `PostalCode`, `CountryCode`, validator interfaces, built-in validator factory, and built-in country validators.
- `src/Addressing.Spain` - Spain-specific extension package that registers `SpanishAddressValidator` into the validator factory.
- `tests/Addressing.Tests` - xUnit tests covering the built-in validators for the US, UK, and Canada.
- `ManualTestRig` - Console app demonstrating service registration and runtime validation.

## Architecture

The solution is organized around a small set of value objects and a validator-factory pattern:

- `Address` captures line, city, province/state, postal code, and country.
- `CountryCode` validates and normalizes ISO 3166-1 alpha-2 codes.
- `PostalCode` couples a postal code value with a country.
- `IAddressValidator` implementations enforce country-specific rules.
- `IAddressValidatorFactory` resolves validators by country code.
- `AddAddressing(...)` registers built-in validators for selected countries.
- `AddSpainAddressing()` adds the Spain validator through a deferred startup-action mechanism.

The current built-in validator coverage appears to be:

- `US`
- `GB`
- `CA`
- `ES` via the extension package

## Usage

### Register built-in validators

```csharp
using Addressing;
using Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddAddressing(CountryCode.Parse("GB"), CountryCode.Parse("US"), CountryCode.Parse("CA"));

var serviceProvider = services.BuildServiceProvider();
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

### Register the Spain extension

```csharp
using Addressing;
using Addressing.Spain;
using Addressing.Utilities;
using Addressing.Validation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddAddressing(CountryCode.Parse("GB"), CountryCode.Parse("US"));
services.AddSpainAddressing();

var serviceProvider = services.BuildServiceProvider();

foreach (var action in serviceProvider.GetServices<IStartupAction>())
{
    action.Execute();
}

var validatorFactory = serviceProvider.GetRequiredService<IAddressValidatorFactory>();

var spanishAddress = new Address(
    "123 Main St",
    null,
    "Madrid",
    null,
    new PostalCode("28001", CountryCode.Parse("ES")),
    CountryCode.Parse("ES"));

validatorFactory.GetValidator(spanishAddress.CountryCode).Validate(spanishAddress);
```

## Apparent release readiness gaps

To move this repository toward an initial release, the highest-value next steps appear to be:

1. Add CI for `dotnet build` and `dotnet test` so every change is validated automatically.
2. Add tests for `Addressing.Spain` and for the DI/startup-action registration flow, not just direct validator tests.
3. Align and refresh package documentation so the package README and repository README match the actual API and usage model.
4. Confirm package metadata and release flow for NuGet publishing, including versioning strategy and release notes.
5. Add examples for common consumer scenarios, especially DI configuration and extension-package registration.
6. Consider simplifying or documenting the startup-action workaround used by `AddSpainAddressing()` so extension registration is easier to understand.

## Observations from the current codebase

- The core package is configured to generate a NuGet package from `src/Addressing/Addressing.csproj`.
- The package-level README already exists at `src/Addressing/README.md`, but a repository-level README was previously missing.
- The manual test rig is useful for exploration but should not be treated as a substitute for automated tests.
- The current solution already has a sensible separation between core functionality, extensions, tests, and sample usage.

## License

The repository includes `LICENSE.txt`.
