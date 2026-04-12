using ISOCodex.Addressing.Validation;

namespace ISOCodex.Addressing.Tests;

public class AddressValidatorFactoryTests
{
    [Fact]
    public void RegisterValidator_ThenGetValidator_ReturnsRegisteredInstance()
    {
        var factory = new AddressValidatorFactory();
        var validator = new TestAddressValidator();

        factory.RegisterValidator(CountryCode.Parse("GB"), validator);

        var result = factory.GetValidator(CountryCode.Parse("GB"));

        Assert.Same(validator, result);
    }

    [Fact]
    public void GetValidator_WhenNotRegistered_ThrowsInvalidOperationException()
    {
        var factory = new AddressValidatorFactory();

        var ex = Assert.Throws<InvalidOperationException>(
            () => factory.GetValidator(CountryCode.Parse("GB")));

        Assert.Contains("GB", ex.Message);
    }

    private sealed class TestAddressValidator : IAddressValidator
    {
        public void Validate(Address address)
        {
        }
    }
}
