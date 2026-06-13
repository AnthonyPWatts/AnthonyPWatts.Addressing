using System;

namespace ISOCodex.Addressing.Formatting
{
    internal sealed class AddressFormatterFallbackRegistration
    {
        public AddressFormatterFallbackRegistration(Func<ICountryAddressFormatter> createFormatter)
        {
            CreateFormatter = createFormatter;
        }

        public Func<ICountryAddressFormatter> CreateFormatter { get; }
    }
}
