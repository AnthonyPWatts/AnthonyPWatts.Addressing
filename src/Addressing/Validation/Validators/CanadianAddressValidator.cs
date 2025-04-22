using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Addressing.Validation.Validators
{
    public class CanadianAddressValidator : IAddressValidator
    {
        private static readonly Regex PostalCodeRegex = new Regex(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", RegexOptions.Compiled);

        private static readonly HashSet<string> ValidProvinces = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "AB", "BC", "MB", "NB", "NL", "NS", "NT", "NU", "ON", "PE", "QC", "SK", "YT"
        };

        public void Validate(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");

            if (string.IsNullOrWhiteSpace(address.Line1))
                throw new ArgumentException("Line1 cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(address.City))
                throw new ArgumentException("City cannot be null or empty.");

            if (address.CountryCode.Code != "CA")
                throw new ArgumentException("CountryCode must be 'CA' for Canadian addresses.");

            if (!PostalCodeRegex.IsMatch(address.PostalCode.Code))
                throw new ArgumentException("PostalCode must be a valid Canadian postal code (e.g., A1A 1A1).");

            if (!string.IsNullOrWhiteSpace(address.StateOrProvince) &&
                !ValidProvinces.Contains(address.StateOrProvince))
            {
                throw new ArgumentException($"StateOrProvince '{address.StateOrProvince}' is not a valid Canadian province or territory.");
            }
        }
    }
}
