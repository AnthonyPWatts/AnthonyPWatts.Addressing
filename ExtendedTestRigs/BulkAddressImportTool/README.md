# BulkAddressImportTool

Console POC for importing mixed-country address data from CSV, validating rows, formatting accepted rows, and producing review outputs.

## Countries

Uses GB, IE, FR, and ES as country-specific validators/formatters. It also registers generic fallbacks so unsupported-but-valid ISO countries, such as AU in the sample data, can be marked `AcceptedUnverified`.

## Run

```bash
dotnet run --project ExtendedTestRigs/BulkAddressImportTool -- SampleData/addresses.csv
```

Outputs are written under the built app's `Output` directory:

- `import-results.json`
- `import-results.csv`
- `import-summary.txt`

## Features exercised

- CSV parsing with import-level error handling.
- Mixed-country row mapping into `Address`.
- Country-specific validator resolution.
- `Valid`, `Invalid`, `AcceptedUnverified`, and `NotValidated` statuses.
- Validation issue serialization in a persistence-friendly shape.
- Single-line address formatting for accepted rows.

## Known limitations

- The CSV parser is intentionally small and only supports single-line records.
- Output files are written beside the compiled application, not back into source.
- The tool uses a local supported-country list to decide when fallback validation means `AcceptedUnverified`.
