# Release Checklist

Use this checklist before publishing a package to NuGet. NuGet package versions
are immutable, so complete these checks before pushing either package.

## Pre-Publish Checks

- Confirm `Directory.Build.props` contains the intended package version.
- Confirm the version has not already been published for either package ID:
  - `ISOCodex.Addressing`
  - `ISOCodex.Addressing.Spain`
- Commit and push the version change before creating the final packages.
- Confirm the working tree is clean and aligned with `origin/main`.

## Package Verification

Run the release check from the repository root after the release commit has been
pushed:

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

The generated packages are written to `artifacts/release-check`, which is
ignored by Git.

Before publishing, inspect the generated nuspec metadata and confirm:

- both packages use the intended version
- both packages reference the pushed release commit
- `ISOCodex.Addressing.Spain` depends on the same version of `ISOCodex.Addressing`

## Publish Order

Publish the core package first:

```powershell
dotnet nuget push artifacts/release-check/ISOCodex.Addressing.<version>.nupkg --source https://api.nuget.org/v3/index.json --api-key <api-key>
```

Then publish the Spain extension package:

```powershell
dotnet nuget push artifacts/release-check/ISOCodex.Addressing.Spain.<version>.nupkg --source https://api.nuget.org/v3/index.json --api-key <api-key>
```

After publishing, confirm both package IDs list the new version on NuGet.
