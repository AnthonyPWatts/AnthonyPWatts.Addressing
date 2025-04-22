using System;
using System.Collections.Concurrent;

namespace Addressing.Validation
{
    public class AddressValidatorFactory : IAddressValidatorFactory
    {
        private readonly ConcurrentDictionary<string, IAddressValidator> _validators = new ConcurrentDictionary<string, IAddressValidator>();

        public void RegisterValidator(CountryCode countryCode, IAddressValidator validator)
        {
            // Note the overwriting of the existing validator
            _validators[countryCode.Code] = validator;
        }

        public IAddressValidator GetValidator(CountryCode countryCode)
        {
            if (_validators.TryGetValue(countryCode.Code, out var validator))
                return validator;

            throw new InvalidOperationException($"No address validator registered for country code '{countryCode.Code}'.");
        }
    }
}

