using System.Collections.Generic;
using System.Text.RegularExpressions;
using ISOCodex.Addressing.Validation;

namespace ISOCodex.Addressing.Ireland
{
    public class IrelandAddressValidator : IAddressValidator
    {
        private static readonly Regex EircodeRegex =
            new Regex(@"^[A-Z0-9]{3}\s?[A-Z0-9]{4}$", RegexOptions.Compiled);

        public AddressValidationResult Validate(Address? address)
        {
            var issues = new List<AddressValidationIssue>();

            AddCommonIssues(issues, address);

            if (address == null)
            {
                return new AddressValidationResult(issues);
            }

            var postalCode = address.PostalCode.Code;

            if (string.IsNullOrWhiteSpace(postalCode) ||
                !EircodeRegex.IsMatch(postalCode.Trim().ToUpperInvariant()))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.PostalCode.Invalid",
                    "PostalCode must be a valid Irish Eircode (e.g., D02 X285).",
                    nameof(Address.PostalCode)));
            }

            return new AddressValidationResult(issues);
        }

        private static void AddCommonIssues(
            ICollection<AddressValidationIssue> issues,
            Address? address)
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

            if (address.CountryCode != CountryCode.IE)
            {
                issues.Add(new AddressValidationIssue(
                    "Address.CountryCode.Invalid",
                    "CountryCode must be 'IE' for Irish addresses.",
                    nameof(Address.CountryCode)));
            }
        }
    }
}
