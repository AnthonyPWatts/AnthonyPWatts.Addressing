# Extended test rigs

These projects are deliberately consumer-shaped POCs for `ISOCodex.Addressing`. They are not unit tests. They exist to exercise the library from application edges where API roughness, metadata gaps, country-pack inconsistencies, and documentation gaps are easier to see.

## Projects

- `CheckoutAddressApi` is an ASP.NET Core Minimal API for ecommerce checkout address validation, preview formatting, and fake order confirmation.
- `DynamicAddressFormDemo` is a Razor Pages UI that builds an address form from profile metadata and validates posted input.
- `BulkAddressImportTool` is a console import tool that reads mixed-country CSV rows and writes JSON, CSV, and text review outputs.

All projects use local project references rather than NuGet package references.

## Smoke test

From the repository root:

```bash
dotnet build ISOCodex.Addressing.sln
dotnet test
dotnet run --project ExtendedTestRigs/BulkAddressImportTool -- SampleData/addresses.csv
dotnet run --project ExtendedTestRigs/CheckoutAddressApi --urls http://localhost:5000
dotnet run --project ExtendedTestRigs/DynamicAddressFormDemo --urls http://localhost:5001
```

For the web apps, use `CheckoutAddressApi/CheckoutAddressApi.http` and open `http://localhost:5001` for the form demo.

## Working notes

`FINDINGS.md` is intentionally a living notes file. Add friction and suspected defects as they are discovered instead of waiting for polished conclusions.
