# Alpha Release Checklist

Fast-track items before publishing the first alpha packages.

## Done

- [x] Rename solution to `ISOCodex.Addressing.sln`.
- [x] Rename GitHub repository to `AnthonyPWatts/ISOCodex.Addressing`.
- [x] Align package repository metadata with the current GitHub repository.
- [x] Mark `ManualTestRig` as non-packable.
- [x] Set package version to `1.0.0-alpha.1`.
- [x] Fix `build-test-pack` after the solution rename.

## Remaining

- [x] Remove unused core package dependencies, especially `FluentValidation` and `System.Text.Json`, unless they are part of the intended alpha API.
- [ ] Rework Spain registration so `services.AddAddressing(); services.AddSpainAddressing();` is enough, without manually executing startup actions.
- [ ] Decide whether postal-code validators should normalize common input such as lowercase or missing spaces, or document that validators require canonical formatting.
- [ ] Guard against mismatched `Address.CountryCode` and `PostalCode.Country`.
- [ ] Add a release command/checklist that runs clean, restore, Release build, Release tests, pack, and package inspection.
- [ ] Update GitHub Actions for the Node 20 deprecation warning from `actions/checkout@v4` and `actions/setup-dotnet@v4`.
