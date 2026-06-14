using System.Collections.Generic;

namespace ISOCodex.Addressing.Profiles
{
    internal static class AddressProfileDefaults
    {
        private static readonly AddressFieldOption[] UnitedStatesAdministrativeAreas =
        {
            Option("AL", "Alabama"),
            Option("AK", "Alaska"),
            Option("AZ", "Arizona"),
            Option("AR", "Arkansas"),
            Option("CA", "California"),
            Option("CO", "Colorado"),
            Option("CT", "Connecticut"),
            Option("DE", "Delaware"),
            Option("FL", "Florida"),
            Option("GA", "Georgia"),
            Option("HI", "Hawaii"),
            Option("ID", "Idaho"),
            Option("IL", "Illinois"),
            Option("IN", "Indiana"),
            Option("IA", "Iowa"),
            Option("KS", "Kansas"),
            Option("KY", "Kentucky"),
            Option("LA", "Louisiana"),
            Option("ME", "Maine"),
            Option("MD", "Maryland"),
            Option("MA", "Massachusetts"),
            Option("MI", "Michigan"),
            Option("MN", "Minnesota"),
            Option("MS", "Mississippi"),
            Option("MO", "Missouri"),
            Option("MT", "Montana"),
            Option("NE", "Nebraska"),
            Option("NV", "Nevada"),
            Option("NH", "New Hampshire"),
            Option("NJ", "New Jersey"),
            Option("NM", "New Mexico"),
            Option("NY", "New York"),
            Option("NC", "North Carolina"),
            Option("ND", "North Dakota"),
            Option("OH", "Ohio"),
            Option("OK", "Oklahoma"),
            Option("OR", "Oregon"),
            Option("PA", "Pennsylvania"),
            Option("RI", "Rhode Island"),
            Option("SC", "South Carolina"),
            Option("SD", "South Dakota"),
            Option("TN", "Tennessee"),
            Option("TX", "Texas"),
            Option("UT", "Utah"),
            Option("VT", "Vermont"),
            Option("VA", "Virginia"),
            Option("WA", "Washington"),
            Option("WV", "West Virginia"),
            Option("WI", "Wisconsin"),
            Option("WY", "Wyoming"),
            Option("DC", "District of Columbia"),
            Option("AS", "American Samoa"),
            Option("GU", "Guam"),
            Option("MP", "Northern Mariana Islands"),
            Option("PR", "Puerto Rico"),
            Option("VI", "U.S. Virgin Islands")
        };

        private static readonly AddressFieldOption[] CanadaAdministrativeAreas =
        {
            Option("AB", "Alberta"),
            Option("BC", "British Columbia"),
            Option("MB", "Manitoba"),
            Option("NB", "New Brunswick"),
            Option("NL", "Newfoundland and Labrador"),
            Option("NS", "Nova Scotia"),
            Option("NT", "Northwest Territories"),
            Option("NU", "Nunavut"),
            Option("ON", "Ontario"),
            Option("PE", "Prince Edward Island"),
            Option("QC", "Quebec"),
            Option("SK", "Saskatchewan"),
            Option("YT", "Yukon")
        };

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
                    Field(
                        AddressField.AdministrativeArea,
                        "State",
                        true,
                        40,
                        "DC",
                        AddressFieldInputKind.Select,
                        UnitedStatesAdministrativeAreas),
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
                    Field(
                        AddressField.AdministrativeArea,
                        "Province or territory",
                        true,
                        40,
                        "ON",
                        AddressFieldInputKind.Select,
                        CanadaAdministrativeAreas),
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

        private static AddressFieldProfile Field(
            AddressField field,
            string label,
            bool isRequired,
            int displayOrder,
            string? placeholder,
            AddressFieldInputKind inputKind,
            IEnumerable<AddressFieldOption> options)
        {
            return new AddressFieldProfile(
                field,
                label,
                isRequired,
                displayOrder,
                placeholder,
                inputKind: inputKind,
                options: options);
        }

        private static AddressFieldOption Option(string value, string label)
        {
            return new AddressFieldOption(value, label);
        }
    }
}
