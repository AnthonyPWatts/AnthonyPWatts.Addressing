using System.Collections.Generic;

namespace ISOCodex.Addressing.Profiles
{
    internal static class AddressProfileDefaults
    {
        public static AddressProfile CreateGreatBritainProfile()
        {
            return new AddressProfile(
                CountryCode.GB,
                new[]
                {
                    Field(AddressField.AddressLine1, "Address line 1", true, 10, "10 Downing Street"),
                    Field(AddressField.AddressLine2, "Address line 2", false, 20),
                    Field(AddressField.AddressLine3, "Address line 3", false, 30),
                    Field(AddressField.Locality, "Town or city", true, 40, "London"),
                    Field(AddressField.AdministrativeArea, "County", false, 50),
                    Field(AddressField.PostalCode, "Postcode", true, 60, "SW1A 2AA"),
                    Field(AddressField.Country, "Country", true, 70, "United Kingdom")
                },
                examplePostalCode: "SW1A 2AA",
                exampleFormattedAddress: "10 Downing Street\nLondon\nSW1A 2AA\nUnited Kingdom");
        }

        public static AddressProfile CreateUnitedStatesProfile()
        {
            return new AddressProfile(
                CountryCode.US,
                new[]
                {
                    Field(AddressField.AddressLine1, "Street address", true, 10, "1600 Pennsylvania Avenue NW"),
                    Field(AddressField.AddressLine2, "Apartment, suite, unit, building", false, 20, "Apt 4B"),
                    Field(AddressField.Locality, "City", true, 30, "Washington"),
                    Field(AddressField.AdministrativeArea, "State", true, 40, "DC"),
                    Field(AddressField.PostalCode, "ZIP code", true, 50, "20500"),
                    Field(AddressField.Country, "Country", true, 60, "United States")
                },
                examplePostalCode: "20500",
                exampleFormattedAddress: "1600 Pennsylvania Avenue NW\nWashington, DC 20500\nUnited States");
        }

        public static AddressProfile CreateCanadaProfile()
        {
            return new AddressProfile(
                CountryCode.CA,
                new[]
                {
                    Field(AddressField.AddressLine1, "Street address", true, 10, "111 Wellington Street"),
                    Field(AddressField.AddressLine2, "Apartment, suite, unit, building", false, 20),
                    Field(AddressField.Locality, "City or town", true, 30, "Ottawa"),
                    Field(AddressField.AdministrativeArea, "Province or territory", true, 40, "ON"),
                    Field(AddressField.PostalCode, "Postal code", true, 50, "K1A 0A6"),
                    Field(AddressField.Country, "Country", true, 60, "Canada")
                },
                examplePostalCode: "K1A 0A6",
                exampleFormattedAddress: "111 Wellington Street\nOttawa ON K1A 0A6\nCanada");
        }

        public static AddressProfile CreateGenericFallbackProfile()
        {
            return new AddressProfile(
                CountryCode.GB,
                new[]
                {
                    Field(AddressField.AddressLine1, "Address line 1", true, 10),
                    Field(AddressField.AddressLine2, "Address line 2", false, 20),
                    Field(AddressField.AddressLine3, "Address line 3", false, 30),
                    Field(AddressField.Locality, "Town / city / locality", false, 40),
                    Field(AddressField.AdministrativeArea, "State / province / region", false, 50),
                    Field(AddressField.PostalCode, "Postal code", false, 60),
                    Field(AddressField.Country, "Country", true, 70)
                },
                AddressProfileSource.GenericFallback);
        }

        private static AddressFieldProfile Field(
            AddressField field,
            string label,
            bool isRequired,
            int displayOrder,
            string? placeholder = null)
        {
            return new AddressFieldProfile(
                field,
                label,
                isRequired,
                displayOrder,
                placeholder);
        }
    }
}
