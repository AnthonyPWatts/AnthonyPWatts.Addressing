using System;
using System.Collections.Concurrent;

namespace ISOCodex.Addressing.Formatting
{
    public sealed class AddressFormatter : IAddressFormatter
    {
        private readonly ConcurrentDictionary<string, ICountryAddressFormatter> _formatters =
            new ConcurrentDictionary<string, ICountryAddressFormatter>();
        private ICountryAddressFormatter? _fallbackFormatter;

        public void RegisterFormatter(CountryCode countryCode, ICountryAddressFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            _formatters[countryCode.Code] = formatter;
        }

        public void RegisterFallbackFormatter(ICountryAddressFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            _fallbackFormatter = formatter;
        }

        public string Format(Address address, AddressFormatOptions? options = null)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (!_formatters.TryGetValue(address.CountryCode.Code, out var formatter))
            {
                if (_fallbackFormatter == null)
                {
                    throw new InvalidOperationException(
                        $"No address formatter registered for country code '{address.CountryCode.Code}'.");
                }

                formatter = _fallbackFormatter;
            }

            return formatter.Format(address, options);
        }
    }
}
