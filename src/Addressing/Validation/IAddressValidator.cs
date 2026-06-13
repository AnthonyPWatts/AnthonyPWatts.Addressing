namespace ISOCodex.Addressing.Validation
{
    public interface IAddressValidator
    {
        AddressValidationResult Validate(Address? address);
    }
}
