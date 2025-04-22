using System;
using System.Text.RegularExpressions;

namespace Addressing.Validation.Validators
{
    public class UKAddressValidator : IAddressValidator
    {
        private static readonly Regex PostcodeRegex = new Regex(
            @"^(GIR 0AA|[A-Z]{1,2}[0-9][0-9A-Z]? ?[0-9][A-Z]{2})$",
            RegexOptions.Compiled);


        public void Validate(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");

            if (string.IsNullOrWhiteSpace(address.Line1))
                throw new ArgumentException("Line1 cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(address.City))
                throw new ArgumentException("City cannot be null or empty.");

            if (address.CountryCode.Code != "GB")
                throw new ArgumentException("CountryCode must be 'GB' for UK addresses.");

            if (!PostcodeRegex.IsMatch(address.PostalCode.Code))
                throw new ArgumentException("PostalCode must be a valid UK postcode (e.g., SW1A 1AA).");
        }
    }
}
