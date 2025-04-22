using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Addressing.Validation.Validators
{
    public class USAddressValidator : IAddressValidator
    {
        private static readonly Regex ZipCodeRegex = new Regex(@"^\d{5}(-\d{4})?$", RegexOptions.Compiled);

        private static readonly HashSet<string> ValidStates = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME",
                "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA",
                "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "DC", "AS", "GU", "MP", "PR", "VI"
            };

        public void Validate(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");

            if (string.IsNullOrWhiteSpace(address.Line1))
                throw new ArgumentException("Line1 cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(address.City))
                throw new ArgumentException("City cannot be null or empty.");

            if (address.CountryCode.Code != "US")
                throw new ArgumentException("CountryCode must be 'US' for US addresses.");

            if (!ZipCodeRegex.IsMatch(address.PostalCode.Code))
                throw new ArgumentException("PostalCode must be a valid US ZIP code (e.g., 12345 or 12345-6789).");

            if (!string.IsNullOrWhiteSpace(address.StateOrProvince) &&
                !ValidStates.Contains(address.StateOrProvince))
            {
                throw new ArgumentException($"StateOrProvince '{address.StateOrProvince}' is not a valid US state or territory.");
            }
        }
    }
}
