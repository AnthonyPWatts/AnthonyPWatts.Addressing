using System;
using System.Collections.Concurrent;

namespace ISOCodex.Addressing.Formatting
{
    public sealed class AddressFormatter : IAddressFormatter
    {
        private readonly ConcurrentDictionary<string, ICountryAddressFormatter> _formatters =
            new ConcurrentDictionary<string, ICountryAddressFormatter>();

        public void RegisterFormatter(CountryCode countryCode, ICountryAddressFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            _formatters[countryCode.Code] = formatter;
        }

        public string Format(Address address, AddressFormatOptions? options = null)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (!_formatters.TryGetValue(address.CountryCode.Code, out var formatter))
            {
                throw new InvalidOperationException(
                    $"No address formatter registered for country code '{address.CountryCode.Code}'.");
            }

            return formatter.Format(address, options);
        }
    }
}
