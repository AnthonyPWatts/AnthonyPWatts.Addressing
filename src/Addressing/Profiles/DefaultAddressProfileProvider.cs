using System;
using System.Collections.Concurrent;

namespace ISOCodex.Addressing.Profiles
{
    public sealed class DefaultAddressProfileProvider : IAddressProfileProvider
    {
        private readonly ConcurrentDictionary<string, AddressProfile> _profiles =
            new ConcurrentDictionary<string, AddressProfile>();
        private AddressProfile? _fallbackProfile;

        public void RegisterProfile(CountryCode countryCode, AddressProfile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            _profiles[countryCode.Code] = profile.ForCountry(countryCode);
        }

        public void RegisterFallbackProfile(AddressProfile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            _fallbackProfile = profile;
        }

        public AddressProfile GetProfile(CountryCode countryCode)
        {
            if (_profiles.TryGetValue(countryCode.Code, out var profile))
            {
                return profile;
            }

            if (_fallbackProfile != null)
            {
                return _fallbackProfile.ForCountry(countryCode);
            }

            throw new InvalidOperationException(
                $"No address profile registered for country code '{countryCode.Code}'.");
        }
    }
}
