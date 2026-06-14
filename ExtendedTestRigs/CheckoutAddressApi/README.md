# CheckoutAddressApi

Minimal API POC for an ecommerce checkout service that accepts billing and shipping addresses from international customers.

## Countries

Uses GB, US, CA, ES, FR, and IE. Each country is registered through a country package; the core package supplies only the shared registries and abstractions.

## Run

```bash
dotnet run --project ExtendedTestRigs/CheckoutAddressApi --urls http://localhost:5000
```

Then use `CheckoutAddressApi.http` or call the endpoints directly.

## Features exercised

- Multi-country dependency injection registration.
- Country-specific validator resolution.
- Structured API validation errors.
- Multi-line and single-line formatting.
- Address profile metadata for frontend form generation.
- Valid, invalid, missing-field, and unsupported-country samples.

## Known limitations

- There is no persistence or real order workflow.
- The API has to do pre-validation before constructing `Address` because the domain model throws for missing line 1, city, and postal code.
- Supported countries are listed locally because the library does not expose a registered-country catalogue.
