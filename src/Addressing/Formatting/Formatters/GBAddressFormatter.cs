using System.Collections.Generic;

namespace ISOCodex.Addressing.Formatting.Formatters
{
    public sealed class GBAddressFormatter : ICountryAddressFormatter
    {
        public string Format(Address address, AddressFormatOptions? options = null)
        {
            var lines = new List<string>
            {
                address.Line1
            };

            AddressFormatting.AddIfNotWhiteSpace(lines, address.Line2);
            AddressFormatting.AddIfNotWhiteSpace(lines, address.City);
            AddressFormatting.AddIfNotWhiteSpace(lines, address.PostalCode.Code);

            return AddressFormatting.FormatLines(lines, "United Kingdom", options);
        }
    }
}
