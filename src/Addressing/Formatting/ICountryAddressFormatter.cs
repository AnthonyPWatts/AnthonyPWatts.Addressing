namespace ISOCodex.Addressing.Formatting
{
    public interface ICountryAddressFormatter
    {
        string Format(Address address, AddressFormatOptions? options = null);
    }
}
