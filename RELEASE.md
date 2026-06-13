# Release Checklist

Use this checklist before publishing an alpha package.

## Alpha Verification

Run the release check from the repository root:

```powershell
pwsh ./scripts/release-check.ps1
```

The script performs:

- clean `Release` outputs
- restore
- Release build
- Release tests
- solution pack
- package count/name inspection
- package content inspection for readme, nuspec, and `netstandard2.1` library output

Expected packages:

- `ISOCodex.Addressing.1.0.0-alpha.2.nupkg`
- `ISOCodex.Addressing.Spain.1.0.0-alpha.2.nupkg`

The generated packages are written to `artifacts/release-check`, which is ignored by Git.
