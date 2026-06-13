using System;

namespace ISOCodex.Addressing.Profiles
{
    internal sealed class AddressProfileRegistration
    {
        public AddressProfileRegistration(
            CountryCode country,
            Func<AddressProfile> createProfile)
        {
            Country = country;
            CreateProfile = createProfile;
        }

        public CountryCode Country { get; }

        public Func<AddressProfile> CreateProfile { get; }
    }
}
