namespace ISOCodex.Addressing.Formatting
{
    public interface IAddressFormatter
    {
        string Format(Address address, AddressFormatOptions? options = null);
    }
}
