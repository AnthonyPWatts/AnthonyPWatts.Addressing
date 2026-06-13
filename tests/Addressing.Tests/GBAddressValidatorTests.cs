using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class GBAddressValidatorTests
{
    private readonly GBAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_DoesNotThrow()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA"),
            CountryCode.GB);

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_WithLowercasePostcodeWithoutSpace_DoesNotThrow()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("sw1a2aa"),
            CountryCode.GB);

        _validator.Validate(address);

        Assert.Equal("sw1a2aa", address.PostalCode.Code);
    }

    [Fact]
    public void Validate_WithInvalidPostcode_Throws()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("BADCODE"),
            CountryCode.GB);

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
