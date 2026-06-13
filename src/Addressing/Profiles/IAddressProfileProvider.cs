namespace ISOCodex.Addressing.Profiles
{
    public interface IAddressProfileProvider
    {
        AddressProfile GetProfile(CountryCode countryCode);
    }
}
