namespace Addressing.Validation
{
    public interface IAddressValidatorFactory
    {
        IAddressValidator GetValidator(CountryCode countryCode);
        void RegisterValidator(CountryCode countryCode, IAddressValidator validator);
    }
}
