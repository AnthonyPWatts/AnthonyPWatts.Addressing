using System.Collections.Generic;
using System.Linq;
using ISOCodex.Addressing.Formatting;

namespace ISOCodex.Addressing.Spain
{
    public sealed class SpanishAddressFormatter : ICountryAddressFormatter
    {
        public string Format(Address address, AddressFormatOptions? options = null)
        {
            var lines = new List<string>
            {
                address.Line1
            };

            AddIfNotWhiteSpace(lines, address.Line2);
            AddIfNotWhiteSpace(
                lines,
                JoinParts(" ", address.PostalCode.Code, address.City));

            return FormatLines(lines, "Spain", options);
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

        private static string JoinParts(string separator, params string?[] parts)
        {
            return string.Join(
                separator,
                parts
                    .Where(part => !string.IsNullOrWhiteSpace(part))
                    .Select(part => part!.Trim()));
        }
    }
}
