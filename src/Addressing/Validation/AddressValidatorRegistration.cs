using System;

namespace ISOCodex.Addressing.Validation
{
    internal sealed class AddressValidatorRegistration
    {
        public AddressValidatorRegistration(
            CountryCode country,
            Func<IAddressValidator> createValidator)
        {
            Country = country;
            CreateValidator = createValidator;
        }

        public CountryCode Country { get; }

        public Func<IAddressValidator> CreateValidator { get; }
    }
}
