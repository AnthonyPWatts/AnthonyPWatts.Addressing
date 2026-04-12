using ISOCodex.Addressing.Validation.Validators;

namespace ISOCodex.Addressing.Tests;

public class UKAddressValidatorTests
{
    private readonly UKAddressValidator _validator = new();

    [Fact]
    public void Validate_WithValidAddress_DoesNotThrow()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("SW1A 2AA", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        _validator.Validate(address);
    }

    [Fact]
    public void Validate_WithInvalidPostcode_Throws()
    {
        var address = new Address(
            "10 Downing St",
            null,
            "London",
            null,
            new PostalCode("BADCODE", CountryCode.Parse("GB")),
            CountryCode.Parse("GB"));

        Assert.Throws<ArgumentException>(() => _validator.Validate(address));
    }
}
