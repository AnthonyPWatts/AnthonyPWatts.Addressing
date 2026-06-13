using System;

namespace ISOCodex.Addressing.Formatting
{
    internal sealed class AddressFormatterRegistration
    {
        public AddressFormatterRegistration(
            CountryCode country,
            Func<ICountryAddressFormatter> createFormatter)
        {
            Country = country;
            CreateFormatter = createFormatter;
        }

        public CountryCode Country { get; }

        public Func<ICountryAddressFormatter> CreateFormatter { get; }
    }
}
