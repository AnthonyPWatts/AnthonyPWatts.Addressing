using System;

namespace ISOCodex.Addressing.Validation
{
    internal sealed class AddressValidatorFallbackRegistration
    {
        public AddressValidatorFallbackRegistration(Func<IAddressValidator> createValidator)
        {
            CreateValidator = createValidator;
        }

        public Func<IAddressValidator> CreateValidator { get; }
    }
}
