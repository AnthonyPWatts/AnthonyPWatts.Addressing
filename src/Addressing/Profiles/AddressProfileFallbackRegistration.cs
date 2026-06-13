using System;

namespace ISOCodex.Addressing.Profiles
{
    internal sealed class AddressProfileFallbackRegistration
    {
        public AddressProfileFallbackRegistration(Func<AddressProfile> createProfile)
        {
            CreateProfile = createProfile;
        }

        public Func<AddressProfile> CreateProfile { get; }
    }
}
