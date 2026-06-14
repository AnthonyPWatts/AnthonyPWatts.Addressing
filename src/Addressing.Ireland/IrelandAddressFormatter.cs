using System.Collections.Generic;
using System.Linq;
using ISOCodex.Addressing.Formatting;

namespace ISOCodex.Addressing.Ireland
{
    public sealed class IrelandAddressFormatter : ICountryAddressFormatter
    {
        public string Format(Address address, AddressFormatOptions? options = null)
        {
            var lines = new List<string>
            {
                address.Line1
            };

            AddIfNotWhiteSpace(lines, address.Line2);
            AddIfNotWhiteSpace(lines, address.City);
            AddIfNotWhiteSpace(lines, address.PostalCode.Code);

            return FormatLines(lines, "Ireland", options);
        }

        private static void AddIfNotWhiteSpace(ICollection<string> lines, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                lines.Add(value.Trim());
            }
        }

        private static string FormatLines(
            ICollection<string> lines,
            string countryName,
            AddressFormatOptions? options)
        {
            var effectiveOptions = options ?? new AddressFormatOptions();
            var formattedLines = lines.ToList();

            if (effectiveOptions.IncludeCountry)
            {
                AddIfNotWhiteSpace(formattedLines, countryName);
            }

            if (effectiveOptions.Style == AddressFormatStyle.SingleLine)
            {
                return string.Join(effectiveOptions.SingleLineSeparator, formattedLines);
            }

            return string.Join("\n", formattedLines);
        }
    }
}
