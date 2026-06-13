using System;
using System.Collections.Concurrent;

namespace ISOCodex.Addressing.Validation
{
    public class AddressValidatorFactory : IAddressValidatorFactory
    {
        private readonly ConcurrentDictionary<string, IAddressValidator> _validators =
            new ConcurrentDictionary<string, IAddressValidator>();
        private IAddressValidator? _fallbackValidator;

        public void RegisterValidator(CountryCode countryCode, IAddressValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            _validators[countryCode.Code] = validator;
        }

        public void RegisterFallbackValidator(IAddressValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            _fallbackValidator = validator;
        }

        public IAddressValidator GetValidator(CountryCode countryCode)
        {
            if (_validators.TryGetValue(countryCode.Code, out var validator))
            {
                return validator;
            }

            if (_fallbackValidator != null)
            {
                return _fallbackValidator;
            }

            throw new InvalidOperationException(
                $"No address validator registered for country code '{countryCode.Code}'.");
        }
    }
}
