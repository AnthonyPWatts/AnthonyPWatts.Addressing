using System.Collections.Generic;

namespace ISOCodex.Addressing.Validation
{
    internal static class AddressValidationIssues
    {
        public static void AddCommonIssues(
            ICollection<AddressValidationIssue> issues,
            Address? address,
            CountryCode expectedCountry,
            string countryName)
        {
            if (address == null)
            {
                issues.Add(new AddressValidationIssue(
                    "Address.Required",
                    "Address cannot be null."));
                return;
            }

            if (string.IsNullOrWhiteSpace(address.Line1))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.Line1.Required",
                    "Line1 cannot be null or empty.",
                    nameof(Address.Line1)));
            }

            if (string.IsNullOrWhiteSpace(address.City))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.City.Required",
                    "City cannot be null or empty.",
                    nameof(Address.City)));
            }

            if (address.CountryCode != expectedCountry)
            {
                issues.Add(new AddressValidationIssue(
                    "Address.CountryCode.Invalid",
                    $"CountryCode must be '{expectedCountry.Code}' for {countryName} addresses.",
                    nameof(Address.CountryCode)));
            }
        }

    }
}
