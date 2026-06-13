using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ISOCodex.Addressing.Validation.Validators
{
    public class GBAddressValidator : IAddressValidator
    {
        private static readonly Regex PostcodeRegex = new Regex(
            @"^(GIR 0AA|[A-Z]{1,2}[0-9][0-9A-Z]? [0-9][A-Z]{2})$",
            RegexOptions.Compiled);

        public AddressValidationResult Validate(Address? address)
        {
            var issues = new List<AddressValidationIssue>();
            AddressValidationIssues.AddCommonIssues(issues, address, CountryCode.GB, "GB");

            if (address == null)
            {
                return new AddressValidationResult(issues);
            }

            var normalizedPostcode = NormalizePostcode(address.PostalCode.Code);

            if (!PostcodeRegex.IsMatch(normalizedPostcode))
            {
                issues.Add(new AddressValidationIssue(
                    "Address.PostalCode.Invalid",
                    "PostalCode must be a valid GB postcode (e.g., SW1A 1AA).",
                    nameof(Address.PostalCode)));
            }

            return new AddressValidationResult(issues);
        }

        private static string NormalizePostcode(string postcode)
        {
            var compactPostcode = postcode
                .Replace(" ", string.Empty)
                .ToUpperInvariant();

            if (compactPostcode.Length <= 3)
            {
                return compactPostcode;
            }

            return compactPostcode.Insert(compactPostcode.Length - 3, " ");
        }
    }
}
